using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OncoTimeline.Application.DTOs;
using OncoTimeline.Domain.Entities;
using OncoTimeline.Domain.Interfaces;

namespace OncoTimeline.Application.Services;

public class DrugService
{
    private readonly IDrugRepository _drugRepository;

    public DrugService(IDrugRepository drugRepository)
    {
        _drugRepository = drugRepository;
    }

    public async Task<IEnumerable<DrugDetailDto>> GetAllDrugsAsync()
    {
        var drugs = await _drugRepository.GetAllAsync();
        return drugs.Select(MapToDetailDto);
    }

    public async Task<DrugDetailDto?> GetDrugByIdAsync(Guid id)
    {
        var drug = await _drugRepository.GetByIdAsync(id);
        return drug == null ? null : MapToDetailDto(drug);
    }

    public async Task<DrugDetailDto?> GetDrugByNameAsync(string name)
    {
        var drug = await _drugRepository.GetByNameAsync(name);
        return drug == null ? null : MapToDetailDto(drug);
    }

    public async Task<DrugDetailDto> CreateDrugAsync(CreateDrugDto dto)
    {
        var drug = new Drug
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            DrugClass = dto.DrugClass,
            MechanismOfAction = dto.MechanismOfAction,
            WhyUsedInLeukemia = dto.WhyUsedInLeukemia,
            ParentFriendlyExplanation = dto.ParentFriendlyExplanation,
            TypicalOnsetTiming = dto.TypicalOnsetTiming,
            DurationOfEffects = dto.DurationOfEffects,
            ExpectedLabChanges = dto.ExpectedLabChanges,
            NeurologicalImpacts = dto.NeurologicalImpacts,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _drugRepository.AddAsync(drug);
        return MapToDetailDto(created);
    }

    private DrugDetailDto MapToDetailDto(Drug d)
    {
        return new DrugDetailDto(
            d.Id,
            d.Name,
            d.DrugClass,
            d.MechanismOfAction,
            d.WhyUsedInLeukemia,
            d.ParentFriendlyExplanation,
            d.TypicalOnsetTiming,
            d.DurationOfEffects,
            d.ExpectedLabChanges,
            d.NeurologicalImpacts,
            d.TypicalTimeline,
            d.MonitoringReason,
            d.CommonButNotDangerous,
            d.BloodCountPattern,
            d.IsCurrentlyRelevant,
            d.SideEffects.Select(se => new SideEffectDto(
                se.Id,
                se.EffectName,
                se.Severity,
                se.Description,
                se.TypicalOnset
            )).ToList()
        );
    }
}
