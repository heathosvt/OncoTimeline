using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OncoTimeline.Application.DTOs;
using OncoTimeline.Domain.Entities;
using OncoTimeline.Domain.Interfaces;

namespace OncoTimeline.Application.Services;

public class TreatmentPhaseService
{
    private readonly ITreatmentPhaseRepository _repository;

    public TreatmentPhaseService(ITreatmentPhaseRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TreatmentPhaseDto>> GetByPatientIdAsync(Guid patientId)
    {
        var phases = await _repository.GetByPatientIdAsync(patientId);
        return phases.Select(MapToDto);
    }

    public async Task<TreatmentPhaseDto> CreateAsync(CreateTreatmentPhaseDto dto)
    {
        var phase = new TreatmentPhase
        {
            Id = Guid.NewGuid(),
            PatientId = dto.PatientId,
            Name = dto.Name,
            Description = dto.Description,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            DisplayOrder = dto.DisplayOrder,
            Color = dto.Color
        };

        var created = await _repository.AddAsync(phase);
        return MapToDto(created);
    }

    private TreatmentPhaseDto MapToDto(TreatmentPhase p)
    {
        return new TreatmentPhaseDto(
            p.Id,
            p.PatientId,
            p.Name,
            p.Description,
            p.StartDate,
            p.EndDate,
            p.DisplayOrder,
            p.Color
        );
    }
}
