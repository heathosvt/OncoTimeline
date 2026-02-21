using System;

namespace OncoTimeline.Domain.Entities;

public class LabResult
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public DateTime LabDate { get; set; }
    public decimal? WBC { get; set; }
    public decimal? ANC { get; set; }
    public decimal? Hemoglobin { get; set; }
    public decimal? Platelets { get; set; }
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    
    public Patient Patient { get; set; } = null!;
}
