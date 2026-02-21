using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OncoTimeline.Domain.Entities;

namespace OncoTimeline.Domain.Interfaces;

public interface IPatientRepository
{
    Task<Patient?> GetByIdAsync(Guid id);
    Task<IEnumerable<Patient>> GetAllAsync();
    Task<Patient> AddAsync(Patient patient);
    Task UpdateAsync(Patient patient);
    Task DeleteAsync(Guid id);
}

public interface ITimelineEventRepository
{
    Task<TimelineEvent?> GetByIdAsync(Guid id);
    Task<IEnumerable<TimelineEvent>> GetByPatientIdAsync(Guid patientId);
    Task<IEnumerable<TimelineEvent>> GetByDateRangeAsync(Guid patientId, DateTime startDate, DateTime endDate);
    Task<TimelineEvent> AddAsync(TimelineEvent timelineEvent);
    Task UpdateAsync(TimelineEvent timelineEvent);
    Task DeleteAsync(Guid id);
}

public interface IDrugRepository
{
    Task<Drug?> GetByIdAsync(Guid id);
    Task<IEnumerable<Drug>> GetAllAsync();
    Task<Drug?> GetByNameAsync(string name);
    Task<Drug> AddAsync(Drug drug);
    Task UpdateAsync(Drug drug);
}

public interface IAIKnowledgeRepository
{
    Task<AIKnowledgeArticle?> GetByIdAsync(Guid id);
    Task<IEnumerable<AIKnowledgeArticle>> GetAllAsync();
    Task<IEnumerable<AIKnowledgeArticle>> GetByCategoryAsync(string category);
    Task<IEnumerable<AIKnowledgeArticle>> GetByAudienceAsync(string audience);
    Task<AIKnowledgeArticle> AddAsync(AIKnowledgeArticle article);
    Task UpdateAsync(AIKnowledgeArticle article);
}

public interface ITreatmentPhaseRepository
{
    Task<TreatmentPhase?> GetByIdAsync(Guid id);
    Task<IEnumerable<TreatmentPhase>> GetByPatientIdAsync(Guid patientId);
    Task<TreatmentPhase> AddAsync(TreatmentPhase phase);
    Task UpdateAsync(TreatmentPhase phase);
}
