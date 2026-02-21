using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OncoTimeline.Domain.Entities;
using OncoTimeline.Domain.Interfaces;
using OncoTimeline.Infrastructure.Data;

namespace OncoTimeline.Infrastructure.Repositories;

public class TimelineEventRepository : ITimelineEventRepository
{
    private readonly OncoTimelineDbContext _context;

    public TimelineEventRepository(OncoTimelineDbContext context)
    {
        _context = context;
    }

    public async Task<TimelineEvent?> GetByIdAsync(Guid id)
    {
        return await _context.TimelineEvents
            .Include(te => te.TimelineEventDrugs)
            .ThenInclude(ted => ted.Drug)
            .FirstOrDefaultAsync(te => te.Id == id);
    }

    public async Task<IEnumerable<TimelineEvent>> GetByPatientIdAsync(Guid patientId)
    {
        return await _context.TimelineEvents
            .Include(te => te.TimelineEventDrugs)
            .ThenInclude(ted => ted.Drug)
            .Where(te => te.PatientId == patientId)
            .OrderBy(te => te.EventDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<TimelineEvent>> GetByDateRangeAsync(Guid patientId, DateTime startDate, DateTime endDate)
    {
        return await _context.TimelineEvents
            .Include(te => te.TimelineEventDrugs)
            .ThenInclude(ted => ted.Drug)
            .Where(te => te.PatientId == patientId && te.EventDate >= startDate && te.EventDate <= endDate)
            .OrderBy(te => te.EventDate)
            .ToListAsync();
    }

    public async Task<TimelineEvent> AddAsync(TimelineEvent timelineEvent)
    {
        _context.TimelineEvents.Add(timelineEvent);
        await _context.SaveChangesAsync();
        return timelineEvent;
    }

    public async Task UpdateAsync(TimelineEvent timelineEvent)
    {
        _context.TimelineEvents.Update(timelineEvent);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.TimelineEvents.FindAsync(id);
        if (entity != null)
        {
            _context.TimelineEvents.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}

public class DrugRepository : IDrugRepository
{
    private readonly OncoTimelineDbContext _context;

    public DrugRepository(OncoTimelineDbContext context)
    {
        _context = context;
    }

    public async Task<Drug?> GetByIdAsync(Guid id)
    {
        return await _context.Drugs
            .Include(d => d.SideEffects)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<IEnumerable<Drug>> GetAllAsync()
    {
        return await _context.Drugs
            .Include(d => d.SideEffects)
            .OrderBy(d => d.Name)
            .ToListAsync();
    }

    public async Task<Drug?> GetByNameAsync(string name)
    {
        return await _context.Drugs
            .Include(d => d.SideEffects)
            .FirstOrDefaultAsync(d => d.Name.ToLower() == name.ToLower());
    }

    public async Task<Drug> AddAsync(Drug drug)
    {
        _context.Drugs.Add(drug);
        await _context.SaveChangesAsync();
        return drug;
    }

    public async Task UpdateAsync(Drug drug)
    {
        _context.Drugs.Update(drug);
        await _context.SaveChangesAsync();
    }
}

public class KnowledgeRepository : IAIKnowledgeRepository
{
    private readonly OncoTimelineDbContext _context;

    public KnowledgeRepository(OncoTimelineDbContext context)
    {
        _context = context;
    }

    public async Task<AIKnowledgeArticle?> GetByIdAsync(Guid id)
    {
        return await _context.KnowledgeArticles.FindAsync(id);
    }

    public async Task<IEnumerable<AIKnowledgeArticle>> GetAllAsync()
    {
        return await _context.KnowledgeArticles.ToListAsync();
    }

    public async Task<IEnumerable<AIKnowledgeArticle>> GetByCategoryAsync(string category)
    {
        return await _context.KnowledgeArticles
            .Where(a => a.Category == category)
            .ToListAsync();
    }

    public async Task<IEnumerable<AIKnowledgeArticle>> GetByAudienceAsync(string audience)
    {
        return await _context.KnowledgeArticles
            .Where(a => a.Audience == audience)
            .ToListAsync();
    }

    public async Task<AIKnowledgeArticle> AddAsync(AIKnowledgeArticle article)
    {
        _context.KnowledgeArticles.Add(article);
        await _context.SaveChangesAsync();
        return article;
    }

    public async Task UpdateAsync(AIKnowledgeArticle article)
    {
        _context.KnowledgeArticles.Update(article);
        await _context.SaveChangesAsync();
    }
}

public class TreatmentPhaseRepository : ITreatmentPhaseRepository
{
    private readonly OncoTimelineDbContext _context;

    public TreatmentPhaseRepository(OncoTimelineDbContext context)
    {
        _context = context;
    }

    public async Task<TreatmentPhase?> GetByIdAsync(Guid id)
    {
        return await _context.TreatmentPhases.FindAsync(id);
    }

    public async Task<IEnumerable<TreatmentPhase>> GetByPatientIdAsync(Guid patientId)
    {
        return await _context.TreatmentPhases
            .Where(p => p.PatientId == patientId)
            .OrderBy(p => p.DisplayOrder)
            .ToListAsync();
    }

    public async Task<TreatmentPhase> AddAsync(TreatmentPhase phase)
    {
        _context.TreatmentPhases.Add(phase);
        await _context.SaveChangesAsync();
        return phase;
    }

    public async Task UpdateAsync(TreatmentPhase phase)
    {
        _context.TreatmentPhases.Update(phase);
        await _context.SaveChangesAsync();
    }
}
