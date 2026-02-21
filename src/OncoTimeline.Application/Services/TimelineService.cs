using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OncoTimeline.Application.DTOs;
using OncoTimeline.Domain.Entities;
using OncoTimeline.Domain.Interfaces;

namespace OncoTimeline.Application.Services;

public class TimelineService
{
    private readonly ITimelineEventRepository _eventRepository;
    private readonly IDrugRepository _drugRepository;

    public TimelineService(ITimelineEventRepository eventRepository, IDrugRepository drugRepository)
    {
        _eventRepository = eventRepository;
        _drugRepository = drugRepository;
    }

    public async Task<IEnumerable<TimelineEventDto>> GetPatientTimelineAsync(Guid patientId)
    {
        var events = await _eventRepository.GetByPatientIdAsync(patientId);
        return events.Select(MapToDto);
    }

    public async Task<IEnumerable<TimelineEventDto>> GetTimelineByDateRangeAsync(Guid patientId, DateTime startDate, DateTime endDate)
    {
        var events = await _eventRepository.GetByDateRangeAsync(patientId, startDate, endDate);
        return events.Select(MapToDto);
    }

    public async Task<TimelineEventDto> CreateEventAsync(CreateTimelineEventDto dto)
    {
        var timelineEvent = new TimelineEvent
        {
            Id = Guid.NewGuid(),
            PatientId = dto.PatientId,
            TreatmentPhaseId = dto.TreatmentPhaseId,
            Title = dto.Title,
            EventDate = dto.EventDate,
            Category = dto.Category,
            Notes = dto.Notes,
            Tags = dto.Tags,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        foreach (var drugDto in dto.Drugs)
        {
            timelineEvent.TimelineEventDrugs.Add(new TimelineEventDrug
            {
                TimelineEventId = timelineEvent.Id,
                DrugId = drugDto.DrugId,
                Dosage = drugDto.Dosage,
                Route = drugDto.Route
            });
        }

        var created = await _eventRepository.AddAsync(timelineEvent);
        return MapToDto(created);
    }

    public async Task<TimelineEventDto> UpdateEventAsync(Guid id, UpdateTimelineEventDto dto)
    {
        var existing = await _eventRepository.GetByIdAsync(id);
        if (existing == null) throw new KeyNotFoundException($"Timeline event {id} not found");

        existing.Title = dto.Title;
        existing.EventDate = dto.EventDate;
        existing.Category = dto.Category;
        existing.Notes = dto.Notes;
        existing.Tags = dto.Tags;
        existing.UpdatedAt = DateTime.UtcNow;

        existing.TimelineEventDrugs.Clear();
        foreach (var drugDto in dto.Drugs)
        {
            existing.TimelineEventDrugs.Add(new TimelineEventDrug
            {
                TimelineEventId = existing.Id,
                DrugId = drugDto.DrugId,
                Dosage = drugDto.Dosage,
                Route = drugDto.Route
            });
        }

        await _eventRepository.UpdateAsync(existing);
        return MapToDto(existing);
    }

    public async Task DeleteEventAsync(Guid id)
    {
        await _eventRepository.DeleteAsync(id);
    }

    private TimelineEventDto MapToDto(TimelineEvent e)
    {
        return new TimelineEventDto(
            e.Id,
            e.PatientId,
            e.TreatmentPhaseId,
            e.Title,
            e.EventDate,
            e.Category,
            e.Notes,
            e.Tags,
            e.TimelineEventDrugs.Select(ted => new DrugDto(
                ted.Drug.Id,
                ted.Drug.Name,
                ted.Drug.DrugClass,
                ted.Drug.ParentFriendlyExplanation,
                ted.Dosage,
                ted.Route
            )).ToList()
        );
    }
}
