using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OncoTimeline.Application.DTOs;
using OncoTimeline.Application.Services;

namespace OncoTimeline.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimelineController : ControllerBase
{
    private readonly TimelineService _timelineService;

    public TimelineController(TimelineService timelineService)
    {
        _timelineService = timelineService;
    }

    [HttpGet("patient/{patientId}")]
    public async Task<ActionResult<IEnumerable<TimelineEventDto>>> GetPatientTimeline(Guid patientId)
    {
        var events = await _timelineService.GetPatientTimelineAsync(patientId);
        return Ok(events);
    }

    [HttpGet("patient/{patientId}/range")]
    public async Task<ActionResult<IEnumerable<TimelineEventDto>>> GetTimelineByDateRange(
        Guid patientId,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        var events = await _timelineService.GetTimelineByDateRangeAsync(patientId, startDate, endDate);
        return Ok(events);
    }

    [HttpPost]
    public async Task<ActionResult<TimelineEventDto>> CreateEvent([FromBody] CreateTimelineEventDto dto)
    {
        var created = await _timelineService.CreateEventAsync(dto);
        return CreatedAtAction(nameof(GetPatientTimeline), new { patientId = created.PatientId }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TimelineEventDto>> UpdateEvent(Guid id, [FromBody] UpdateTimelineEventDto dto)
    {
        try
        {
            var updated = await _timelineService.UpdateEventAsync(id, dto);
            return Ok(updated);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEvent(Guid id)
    {
        await _timelineService.DeleteEventAsync(id);
        return NoContent();
    }
}
