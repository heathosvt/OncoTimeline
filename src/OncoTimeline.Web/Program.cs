using Microsoft.EntityFrameworkCore;
using OncoTimeline.Application.Services;
using OncoTimeline.Domain.Interfaces;
using OncoTimeline.Infrastructure.Data;
using OncoTimeline.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Database - Use in-memory for development
builder.Services.AddDbContext<OncoTimelineDbContext>(options =>
    options.UseInMemoryDatabase("OncoTimeline"));

// Repositories
builder.Services.AddScoped<ITimelineEventRepository, TimelineEventRepository>();
builder.Services.AddScoped<IDrugRepository, DrugRepository>();
builder.Services.AddScoped<IAIKnowledgeRepository, KnowledgeRepository>();
builder.Services.AddScoped<ITreatmentPhaseRepository, TreatmentPhaseRepository>();

// Services
builder.Services.AddScoped<TimelineService>();
builder.Services.AddScoped<DrugService>();
builder.Services.AddScoped<KnowledgeService>();
builder.Services.AddScoped<TreatmentPhaseService>();

// Razor Pages
builder.Services.AddRazorPages();

// Add HttpClient for API calls
builder.Services.AddHttpClient();

var app = builder.Build();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<OncoTimelineDbContext>();
    
    // Seed drugs
    if (!context.Drugs.Any())
    {
        var drugs = DrugSeeder.GetSeedDrugs();
        context.Drugs.AddRange(drugs);
        context.SaveChanges();
    }
    
    // Seed demo patient with phases, events, and knowledge articles
    var demoPatientId = Guid.Parse("00000000-0000-0000-0000-000000000001");
    if (!context.Set<OncoTimeline.Domain.Entities.Patient>().Any(p => p.Id == demoPatientId))
    {
        var patient = new OncoTimeline.Domain.Entities.Patient
        {
            Id = demoPatientId,
            FirstName = "Demo",
            LastName = "Patient",
            DateOfBirth = DateTime.UtcNow.AddYears(-8),
            DiagnosisDate = DateTime.UtcNow.AddMonths(-6),
            DiagnosisType = "B-ALL",
            RiskCategory = "Standard",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        context.Set<OncoTimeline.Domain.Entities.Patient>().Add(patient);
        
        // Add treatment phases
        var inductionPhase = new OncoTimeline.Domain.Entities.TreatmentPhase
        {
            Id = Guid.NewGuid(),
            PatientId = demoPatientId,
            Name = "Induction",
            Description = "Initial intensive treatment phase",
            StartDate = DateTime.UtcNow.AddMonths(-6),
            EndDate = DateTime.UtcNow.AddMonths(-5),
            DisplayOrder = 1,
            Color = "#EF4444"
        };
        var consolidationPhase = new OncoTimeline.Domain.Entities.TreatmentPhase
        {
            Id = Guid.NewGuid(),
            PatientId = demoPatientId,
            Name = "Consolidation",
            Description = "Strengthening treatment phase",
            StartDate = DateTime.UtcNow.AddMonths(-5),
            EndDate = DateTime.UtcNow.AddMonths(-2),
            DisplayOrder = 2,
            Color = "#F59E0B"
        };
        var maintenancePhase = new OncoTimeline.Domain.Entities.TreatmentPhase
        {
            Id = Guid.NewGuid(),
            PatientId = demoPatientId,
            Name = "Maintenance",
            Description = "Long-term maintenance therapy",
            StartDate = DateTime.UtcNow.AddMonths(-2),
            EndDate = null,
            DisplayOrder = 3,
            Color = "#10B981"
        };
        context.Set<OncoTimeline.Domain.Entities.TreatmentPhase>().AddRange(inductionPhase, consolidationPhase, maintenancePhase);
        
        // Add sample timeline events
        var events = new[]
        {
            new OncoTimeline.Domain.Entities.TimelineEvent
            {
                Id = Guid.NewGuid(),
                PatientId = demoPatientId,
                TreatmentPhaseId = inductionPhase.Id,
                Title = "Vincristine Infusion",
                EventDate = DateTime.UtcNow.AddMonths(-6),
                Category = "Chemotherapy",
                Notes = "Day 1 of induction therapy",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new OncoTimeline.Domain.Entities.TimelineEvent
            {
                Id = Guid.NewGuid(),
                PatientId = demoPatientId,
                TreatmentPhaseId = inductionPhase.Id,
                Title = "Lab Work - CBC",
                EventDate = DateTime.UtcNow.AddMonths(-6).AddDays(7),
                Category = "Lab",
                Notes = "Weekly blood count check",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new OncoTimeline.Domain.Entities.TimelineEvent
            {
                Id = Guid.NewGuid(),
                PatientId = demoPatientId,
                TreatmentPhaseId = consolidationPhase.Id,
                Title = "Hospital Admission",
                EventDate = DateTime.UtcNow.AddMonths(-4),
                Category = "Hospital",
                Notes = "Fever management",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new OncoTimeline.Domain.Entities.TimelineEvent
            {
                Id = Guid.NewGuid(),
                PatientId = demoPatientId,
                TreatmentPhaseId = maintenancePhase.Id,
                Title = "Daily Medication",
                EventDate = DateTime.UtcNow.AddDays(-5),
                Category = "Note",
                Notes = "Oral chemo at home",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };
        context.Set<OncoTimeline.Domain.Entities.TimelineEvent>().AddRange(events);
        
        // Add knowledge articles
        var articles = new[]
        {
            new OncoTimeline.Domain.Entities.AIKnowledgeArticle
            {
                Id = Guid.NewGuid(),
                Title = "Understanding Induction Therapy",
                Category = "TreatmentPhase",
                Audience = "NonTechnical",
                Content = "Induction therapy is the first phase of treatment for B-ALL. It typically lasts 4-6 weeks and aims to kill as many leukemia cells as possible to achieve remission. During this time, your child will receive intensive chemotherapy.",
                Summary = "Learn about the first phase of B-ALL treatment",
                IsAIGenerated = true,
                GeneratedAt = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            },
            new OncoTimeline.Domain.Entities.AIKnowledgeArticle
            {
                Id = Guid.NewGuid(),
                Title = "Induction Therapy: Clinical Overview",
                Category = "TreatmentPhase",
                Audience = "Technical",
                Content = "Induction therapy for pediatric B-ALL typically follows COG or DFCI protocols. The regimen includes vincristine, daunorubicin, L-asparaginase, and corticosteroids. Goal is to achieve <5% blasts in bone marrow by day 29.",
                Summary = "Clinical details of induction therapy protocols",
                IsAIGenerated = true,
                GeneratedAt = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            },
            new OncoTimeline.Domain.Entities.AIKnowledgeArticle
            {
                Id = Guid.NewGuid(),
                Title = "Managing Nausea and Vomiting",
                Category = "SideEffect",
                Audience = "NonTechnical",
                Content = "Nausea and vomiting are common side effects of chemotherapy. Your care team will prescribe anti-nausea medications. Small, frequent meals and avoiding strong smells can help. Stay hydrated and call your team if vomiting persists.",
                Summary = "Tips for managing chemotherapy-related nausea",
                IsAIGenerated = true,
                GeneratedAt = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            }
        };
        context.Set<OncoTimeline.Domain.Entities.AIKnowledgeArticle>().AddRange(articles);
        
        context.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
