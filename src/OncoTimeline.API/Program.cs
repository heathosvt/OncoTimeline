using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

// API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS for React frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<OncoTimelineDbContext>();
    
    if (!context.Drugs.Any())
    {
        var drugs = DrugSeeder.GetSeedDrugs();
        context.Drugs.AddRange(drugs);
        context.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
