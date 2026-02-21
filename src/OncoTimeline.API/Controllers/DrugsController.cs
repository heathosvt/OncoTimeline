using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OncoTimeline.Application.DTOs;
using OncoTimeline.Application.Services;

namespace OncoTimeline.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DrugsController : ControllerBase
{
    private readonly DrugService _drugService;

    public DrugsController(DrugService drugService)
    {
        _drugService = drugService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DrugDetailDto>>> GetAllDrugs()
    {
        var drugs = await _drugService.GetAllDrugsAsync();
        return Ok(drugs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DrugDetailDto>> GetDrugById(Guid id)
    {
        var drug = await _drugService.GetDrugByIdAsync(id);
        if (drug == null) return NotFound();
        return Ok(drug);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<DrugDetailDto>> GetDrugByName(string name)
    {
        var drug = await _drugService.GetDrugByNameAsync(name);
        if (drug == null) return NotFound();
        return Ok(drug);
    }

    [HttpPost]
    public async Task<ActionResult<DrugDetailDto>> CreateDrug([FromBody] CreateDrugDto dto)
    {
        var created = await _drugService.CreateDrugAsync(dto);
        return CreatedAtAction(nameof(GetDrugById), new { id = created.Id }, created);
    }
}
