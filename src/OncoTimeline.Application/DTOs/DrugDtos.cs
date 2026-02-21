using System;
using System.Collections.Generic;

namespace OncoTimeline.Application.DTOs;

public record DrugDetailDto(
    Guid Id,
    string Name,
    string DrugClass,
    string MechanismOfAction,
    string WhyUsedInLeukemia,
    string ParentFriendlyExplanation,
    string TypicalOnsetTiming,
    string DurationOfEffects,
    string ExpectedLabChanges,
    string NeurologicalImpacts,
    string TypicalTimeline,
    string MonitoringReason,
    string CommonButNotDangerous,
    string BloodCountPattern,
    bool IsCurrentlyRelevant,
    List<SideEffectDto> SideEffects
);

public record SideEffectDto(
    Guid Id,
    string EffectName,
    string Severity,
    string Description,
    string TypicalOnset
);

public record CreateDrugDto(
    string Name,
    string DrugClass,
    string MechanismOfAction,
    string WhyUsedInLeukemia,
    string ParentFriendlyExplanation,
    string TypicalOnsetTiming,
    string DurationOfEffects,
    string ExpectedLabChanges,
    string NeurologicalImpacts
);
