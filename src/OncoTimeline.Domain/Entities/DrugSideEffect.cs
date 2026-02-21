using System;

namespace OncoTimeline.Domain.Entities;

public class DrugSideEffect
{
    public Guid Id { get; set; }
    public Guid DrugId { get; set; }
    public string EffectName { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty; // "Common", "Serious", "Rare"
    public string Description { get; set; } = string.Empty;
    public string TypicalOnset { get; set; } = string.Empty;
    
    public Drug Drug { get; set; } = null!;
}
