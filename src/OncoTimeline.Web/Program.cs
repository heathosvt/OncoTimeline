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
            FirstName = "Your",
            LastName = "Daughter",
            DateOfBirth = new DateTime(2022, 3, 15), // Nearly 3 years old
            DiagnosisDate = new DateTime(2025, 2, 1), // February 2025
            DiagnosisType = "B-ALL (CNS-Predominant)",
            RiskCategory = "High Risk",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        context.Set<OncoTimeline.Domain.Entities.Patient>().Add(patient);
        
        // Add High-Risk B-ALL treatment phases
        var diagnosisDate = new DateTime(2025, 2, 1);
        
        var inductionPhase = new OncoTimeline.Domain.Entities.TreatmentPhase
        {
            Id = Guid.NewGuid(),
            PatientId = demoPatientId,
            Name = "Induction",
            Description = "Initial intensive treatment - achieve remission (4 weeks)",
            StartDate = diagnosisDate,
            EndDate = diagnosisDate.AddDays(28),
            DisplayOrder = 1,
            Color = "#EF4444"
        };
        var consolidationPhase = new OncoTimeline.Domain.Entities.TreatmentPhase
        {
            Id = Guid.NewGuid(),
            PatientId = demoPatientId,
            Name = "Consolidation",
            Description = "Intensified chemotherapy (4 weeks)",
            StartDate = diagnosisDate.AddDays(29),
            EndDate = diagnosisDate.AddDays(56),
            DisplayOrder = 2,
            Color = "#F59E0B"
        };
        var blinatumomab1Phase = new OncoTimeline.Domain.Entities.TreatmentPhase
        {
            Id = Guid.NewGuid(),
            PatientId = demoPatientId,
            Name = "Blinatumomab #1",
            Description = "Immunotherapy - continuous IV infusion (4 weeks)",
            StartDate = diagnosisDate.AddDays(57),
            EndDate = diagnosisDate.AddDays(84),
            DisplayOrder = 3,
            Color = "#8B5CF6"
        };
        var interimMaint1Phase = new OncoTimeline.Domain.Entities.TreatmentPhase
        {
            Id = Guid.NewGuid(),
            PatientId = demoPatientId,
            Name = "Interim Maintenance 1",
            Description = "Recovery period with lower-intensity chemo (8 weeks)",
            StartDate = diagnosisDate.AddDays(85),
            EndDate = diagnosisDate.AddDays(140),
            DisplayOrder = 4,
            Color = "#06B6D4"
        };
        var blinatumomab2Phase = new OncoTimeline.Domain.Entities.TreatmentPhase
        {
            Id = Guid.NewGuid(),
            PatientId = demoPatientId,
            Name = "Blinatumomab #2",
            Description = "Second immunotherapy cycle (4 weeks)",
            StartDate = diagnosisDate.AddDays(141),
            EndDate = diagnosisDate.AddDays(168),
            DisplayOrder = 5,
            Color = "#8B5CF6"
        };
        var delayedIntensificationPhase = new OncoTimeline.Domain.Entities.TreatmentPhase
        {
            Id = Guid.NewGuid(),
            PatientId = demoPatientId,
            Name = "Delayed Intensification",
            Description = "High-intensity chemotherapy similar to induction (8 weeks)",
            StartDate = diagnosisDate.AddDays(169),
            EndDate = diagnosisDate.AddDays(224),
            DisplayOrder = 6,
            Color = "#DC2626"
        };
        var interimMaint2Phase = new OncoTimeline.Domain.Entities.TreatmentPhase
        {
            Id = Guid.NewGuid(),
            PatientId = demoPatientId,
            Name = "Interim Maintenance 2",
            Description = "Second recovery period (8 weeks)",
            StartDate = diagnosisDate.AddDays(225),
            EndDate = diagnosisDate.AddDays(280),
            DisplayOrder = 7,
            Color = "#06B6D4"
        };
        var maintenancePhase = new OncoTimeline.Domain.Entities.TreatmentPhase
        {
            Id = Guid.NewGuid(),
            PatientId = demoPatientId,
            Name = "Maintenance",
            Description = "Long-term daily oral chemotherapy at home (2+ years)",
            StartDate = diagnosisDate.AddDays(281),
            EndDate = null,
            DisplayOrder = 8,
            Color = "#10B981"
        };
        context.Set<OncoTimeline.Domain.Entities.TreatmentPhase>().AddRange(
            inductionPhase, consolidationPhase, blinatumomab1Phase, interimMaint1Phase,
            blinatumomab2Phase, delayedIntensificationPhase, interimMaint2Phase, maintenancePhase
        );
        
        // Add sample timeline events from Week 1-3 of Induction
        var events = new[]
        {
            new OncoTimeline.Domain.Entities.TimelineEvent
            {
                Id = Guid.NewGuid(),
                PatientId = demoPatientId,
                TreatmentPhaseId = inductionPhase.Id,
                Title = "Diagnosis & ICU Admission",
                EventDate = diagnosisDate,
                Category = "Hospital",
                Notes = "Initial presentation with neurologic symptoms. CNS-predominant B-ALL diagnosed. ICU admission for monitoring.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new OncoTimeline.Domain.Entities.TimelineEvent
            {
                Id = Guid.NewGuid(),
                PatientId = demoPatientId,
                TreatmentPhaseId = inductionPhase.Id,
                Title = "First Vincristine Dose",
                EventDate = diagnosisDate.AddDays(1),
                Category = "Chemotherapy",
                Notes = "Day 1 of induction therapy - Vincristine IV push",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new OncoTimeline.Domain.Entities.TimelineEvent
            {
                Id = Guid.NewGuid(),
                PatientId = demoPatientId,
                TreatmentPhaseId = inductionPhase.Id,
                Title = "Daunorubicin Infusion",
                EventDate = diagnosisDate.AddDays(1),
                Category = "Chemotherapy",
                Notes = "First dose of daunorubicin (red drug). Expect red urine for 1-2 days.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new OncoTimeline.Domain.Entities.TimelineEvent
            {
                Id = Guid.NewGuid(),
                PatientId = demoPatientId,
                TreatmentPhaseId = inductionPhase.Id,
                Title = "Intrathecal Methotrexate",
                EventDate = diagnosisDate.AddDays(2),
                Category = "Chemotherapy",
                Notes = "Spinal tap with chemo into CSF to treat CNS disease",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new OncoTimeline.Domain.Entities.TimelineEvent
            {
                Id = Guid.NewGuid(),
                PatientId = demoPatientId,
                TreatmentPhaseId = inductionPhase.Id,
                Title = "CSF Check - No Blasts",
                EventDate = diagnosisDate.AddDays(5),
                Category = "Lab",
                Notes = "CSF negative for blasts! Excellent early response.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new OncoTimeline.Domain.Entities.TimelineEvent
            {
                Id = Guid.NewGuid(),
                PatientId = demoPatientId,
                TreatmentPhaseId = inductionPhase.Id,
                Title = "Vincristine Dose #2",
                EventDate = diagnosisDate.AddDays(8),
                Category = "Chemotherapy",
                Notes = "Weekly vincristine - watch for jaw pain, constipation",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new OncoTimeline.Domain.Entities.TimelineEvent
            {
                Id = Guid.NewGuid(),
                PatientId = demoPatientId,
                TreatmentPhaseId = inductionPhase.Id,
                Title = "Blood Transfusion",
                EventDate = diagnosisDate.AddDays(10),
                Category = "Hospital",
                Notes = "RBC transfusion for anemia from chemotherapy",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new OncoTimeline.Domain.Entities.TimelineEvent
            {
                Id = Guid.NewGuid(),
                PatientId = demoPatientId,
                TreatmentPhaseId = inductionPhase.Id,
                Title = "Vincristine Dose #3",
                EventDate = diagnosisDate.AddDays(15),
                Category = "Chemotherapy",
                Notes = "Week 3 vincristine - voice weakness noted (expected side effect)",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new OncoTimeline.Domain.Entities.TimelineEvent
            {
                Id = Guid.NewGuid(),
                PatientId = demoPatientId,
                TreatmentPhaseId = inductionPhase.Id,
                Title = "Walking Again!",
                EventDate = diagnosisDate.AddDays(18),
                Category = "Note",
                Notes = "Major milestone - walking almost normally again! Eating well, back to baseline weight.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new OncoTimeline.Domain.Entities.TimelineEvent
            {
                Id = Guid.NewGuid(),
                PatientId = demoPatientId,
                TreatmentPhaseId = inductionPhase.Id,
                Title = "Steroid Taper Complete",
                EventDate = diagnosisDate.AddDays(21),
                Category = "Note",
                Notes = "Dexamethasone taper finished. Heart rate normalized. Mood improving.",
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
