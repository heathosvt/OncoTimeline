using System;

namespace OncoTimeline.Domain.Entities;

public class AIKnowledgeArticle
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; // "TreatmentPhase", "SideEffect", "LabValue", "Procedure", "Recovery"
    public string Audience { get; set; } = "NonTechnical"; // "Technical" or "NonTechnical"
    public string Content { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public bool IsAIGenerated { get; set; }
    public DateTime GeneratedAt { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Disclaimer { get; set; } = "This information is educational and does not replace medical advice from your oncology team.";
}
