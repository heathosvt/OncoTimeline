using System;
using System.Collections.Generic;

namespace OncoTimeline.Domain.Entities;

public class TreatmentPhase
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public string Name { get; set; } = string.Empty; // "Induction", "Consolidation", "Maintenance"
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int DisplayOrder { get; set; }
    public string Color { get; set; } = "#3B82F6"; // Hex color for timeline visualization
    
    public Patient Patient { get; set; } = null!;
    public ICollection<TimelineEvent> TimelineEvents { get; set; } = new List<TimelineEvent>();
}
