using System;
using System.Collections.Generic;

namespace OncoTimeline.Domain.Entities;

public class Patient
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public DateTime DiagnosisDate { get; set; }
    public string DiagnosisType { get; set; } = string.Empty;
    public string RiskCategory { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public ICollection<TreatmentPhase> TreatmentPhases { get; set; } = new List<TreatmentPhase>();
    public ICollection<TimelineEvent> TimelineEvents { get; set; } = new List<TimelineEvent>();
    public ICollection<LabResult> LabResults { get; set; } = new List<LabResult>();
    public ICollection<SymptomEntry> SymptomEntries { get; set; } = new List<SymptomEntry>();
}
