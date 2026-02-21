using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OncoTimeline.Domain.Interfaces;

public interface IAIKnowledgeService
{
    Task<string> GenerateEducationalSummaryAsync(string topic);
    Task<string> ExplainTimelineWeekAsync(Guid patientId, DateTime weekStartDate);
    Task<string> ExplainDrugInteractionAsync(List<string> drugNames);
}
