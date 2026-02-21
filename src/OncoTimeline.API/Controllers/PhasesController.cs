using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OncoTimeline.Application.DTOs;
using OncoTimeline.Application.Services;

namespace OncoTimeline.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhasesController : ControllerBase
{
    private readonly TreatmentPhaseService _service;

    public PhasesController(TreatmentPhaseService service)
    {
        _service = service;
    }

    [HttpGet("patient/{patientId}")]
    public async Task<ActionResult<IEnumerable<TreatmentPhaseDto>>> GetByPatientId(Guid patientId)
    {
        var phases = await _service.GetByPatientIdAsync(patientId);
        return Ok(phases);
    }

    [HttpPost]
    public async Task<ActionResult<TreatmentPhaseDto>> Create([FromBody] CreateTreatmentPhaseDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByPatientId), new { patientId = created.PatientId }, created);
    }
}
