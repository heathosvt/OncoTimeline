using Microsoft.AspNetCore.Mvc.RazorPages;
using OncoTimeline.Application.DTOs;
using OncoTimeline.Application.Services;

namespace OncoTimeline.Web.Pages;

public class TimelineModel : PageModel
{
    private readonly TimelineService _timelineService;
    private readonly TreatmentPhaseService _phaseService;
    
    public List<TimelineEventDto> Events { get; set; } = new();
    public List<TreatmentPhaseDto> Phases { get; set; } = new();
    public string DemoPatientId { get; } = "00000000-0000-0000-0000-000000000001";

    public TimelineModel(TimelineService timelineService, TreatmentPhaseService phaseService)
    {
        _timelineService = timelineService;
        _phaseService = phaseService;
    }

    public async Task OnGetAsync()
    {
        var patientId = Guid.Parse(DemoPatientId);
        Events = (await _timelineService.GetPatientTimelineAsync(patientId)).OrderBy(e => e.EventDate).ToList();
        Phases = (await _phaseService.GetByPatientIdAsync(patientId)).OrderBy(p => p.DisplayOrder).ToList();
    }
}
