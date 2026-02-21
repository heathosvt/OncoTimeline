using Microsoft.EntityFrameworkCore;
using OncoTimeline.Domain.Entities;

namespace OncoTimeline.Infrastructure.Data;

public class OncoTimelineDbContext : DbContext
{
    public OncoTimelineDbContext(DbContextOptions<OncoTimelineDbContext> options) : base(options) { }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<TreatmentPhase> TreatmentPhases => Set<TreatmentPhase>();
    public DbSet<TimelineEvent> TimelineEvents => Set<TimelineEvent>();
    public DbSet<Drug> Drugs => Set<Drug>();
    public DbSet<DrugSideEffect> DrugSideEffects => Set<DrugSideEffect>();
    public DbSet<TimelineEventDrug> TimelineEventDrugs => Set<TimelineEventDrug>();
    public DbSet<LabResult> LabResults => Set<LabResult>();
    public DbSet<SymptomEntry> SymptomEntries => Set<SymptomEntry>();
    public DbSet<AIKnowledgeArticle> AIKnowledgeArticles => Set<AIKnowledgeArticle>();
    public DbSet<AIKnowledgeArticle> KnowledgeArticles => Set<AIKnowledgeArticle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TimelineEventDrug composite key
        modelBuilder.Entity<TimelineEventDrug>()
            .HasKey(ted => new { ted.TimelineEventId, ted.DrugId });

        modelBuilder.Entity<TimelineEventDrug>()
            .HasOne(ted => ted.TimelineEvent)
            .WithMany(te => te.TimelineEventDrugs)
            .HasForeignKey(ted => ted.TimelineEventId);

        modelBuilder.Entity<TimelineEventDrug>()
            .HasOne(ted => ted.Drug)
            .WithMany(d => d.TimelineEventDrugs)
            .HasForeignKey(ted => ted.DrugId);

        // Indexes for performance
        modelBuilder.Entity<TimelineEvent>()
            .HasIndex(te => te.EventDate);

        modelBuilder.Entity<TimelineEvent>()
            .HasIndex(te => te.PatientId);

        modelBuilder.Entity<LabResult>()
            .HasIndex(lr => new { lr.PatientId, lr.LabDate });

        modelBuilder.Entity<SymptomEntry>()
            .HasIndex(se => new { se.PatientId, se.EntryDate });
    }
}
