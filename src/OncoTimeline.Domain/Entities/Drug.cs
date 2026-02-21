using System;
using System.Collections.Generic;

namespace OncoTimeline.Domain.Entities;

public class Drug
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DrugClass { get; set; } = string.Empty;
    public string MechanismOfAction { get; set; } = string.Empty;
    public string WhyUsedInLeukemia { get; set; } = string.Empty;
    public string ParentFriendlyExplanation { get; set; } = string.Empty;
    public string TypicalOnsetTiming { get; set; } = string.Empty;
    public string DurationOfEffects { get; set; } = string.Empty;
    public string ExpectedLabChanges { get; set; } = string.Empty;
    public string NeurologicalImpacts { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public ICollection<DrugSideEffect> SideEffects { get; set; } = new List<DrugSideEffect>();
    public ICollection<TimelineEventDrug> TimelineEventDrugs { get; set; } = new List<TimelineEventDrug>();
}
