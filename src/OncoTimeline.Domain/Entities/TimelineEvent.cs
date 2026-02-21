using System;
using System.Collections.Generic;

namespace OncoTimeline.Domain.Entities;

public class TimelineEvent
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid? TreatmentPhaseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public string Category { get; set; } = string.Empty; // "Chemotherapy", "SpinalTap", "Lab", "Symptom", "Hospitalization", "Note"
    public string Notes { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty; // JSON array or comma-separated
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public Patient Patient { get; set; } = null!;
    public TreatmentPhase? TreatmentPhase { get; set; }
    public ICollection<TimelineEventDrug> TimelineEventDrugs { get; set; } = new List<TimelineEventDrug>();
}
