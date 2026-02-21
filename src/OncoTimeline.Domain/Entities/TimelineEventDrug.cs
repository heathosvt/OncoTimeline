using System;

namespace OncoTimeline.Domain.Entities;

public class TimelineEventDrug
{
    public Guid TimelineEventId { get; set; }
    public Guid DrugId { get; set; }
    public string Dosage { get; set; } = string.Empty;
    public string Route { get; set; } = string.Empty; // "IV", "Oral", "Intrathecal"
    
    public TimelineEvent TimelineEvent { get; set; } = null!;
    public Drug Drug { get; set; } = null!;
}
