using System;

namespace OncoTimeline.Domain.Entities;

public class SymptomEntry
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public DateTime EntryDate { get; set; }
    public decimal? Temperature { get; set; }
    public int? AppetiteLevel { get; set; } // 1-5 scale
    public int? EnergyLevel { get; set; }
    public int? NauseaLevel { get; set; }
    public int? SleepQuality { get; set; }
    public int? MoodLevel { get; set; }
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    
    public Patient Patient { get; set; } = null!;
}
