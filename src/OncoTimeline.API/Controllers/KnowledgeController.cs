using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OncoTimeline.Application.DTOs;
using OncoTimeline.Application.Services;

namespace OncoTimeline.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KnowledgeController : ControllerBase
{
    private readonly KnowledgeService _service;

    public KnowledgeController(KnowledgeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<KnowledgeArticleDto>>> GetAll()
    {
        var articles = await _service.GetAllAsync();
        return Ok(articles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<KnowledgeArticleDto>> GetById(Guid id)
    {
        var article = await _service.GetByIdAsync(id);
        if (article == null) return NotFound();
        return Ok(article);
    }

    [HttpGet("category/{category}")]
    public async Task<ActionResult<IEnumerable<KnowledgeArticleDto>>> GetByCategory(string category)
    {
        var articles = await _service.GetByCategoryAsync(category);
        return Ok(articles);
    }

    [HttpGet("audience/{audience}")]
    public async Task<ActionResult<IEnumerable<KnowledgeArticleDto>>> GetByAudience(string audience)
    {
        var articles = await _service.GetByAudienceAsync(audience);
        return Ok(articles);
    }

    [HttpPost]
    public async Task<ActionResult<KnowledgeArticleDto>> Create([FromBody] CreateKnowledgeArticleDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}
