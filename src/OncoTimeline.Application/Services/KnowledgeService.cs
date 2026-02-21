using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OncoTimeline.Application.DTOs;
using OncoTimeline.Domain.Entities;
using OncoTimeline.Domain.Interfaces;

namespace OncoTimeline.Application.Services;

public class KnowledgeService
{
    private readonly IAIKnowledgeRepository _repository;

    public KnowledgeService(IAIKnowledgeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<KnowledgeArticleDto>> GetAllAsync()
    {
        var articles = await _repository.GetAllAsync();
        return articles.Select(MapToDto);
    }

    public async Task<KnowledgeArticleDto?> GetByIdAsync(Guid id)
    {
        var article = await _repository.GetByIdAsync(id);
        return article == null ? null : MapToDto(article);
    }

    public async Task<IEnumerable<KnowledgeArticleDto>> GetByCategoryAsync(string category)
    {
        var articles = await _repository.GetByCategoryAsync(category);
        return articles.Select(MapToDto);
    }

    public async Task<IEnumerable<KnowledgeArticleDto>> GetByAudienceAsync(string audience)
    {
        var articles = await _repository.GetByAudienceAsync(audience);
        return articles.Select(MapToDto);
    }

    public async Task<KnowledgeArticleDto> CreateAsync(CreateKnowledgeArticleDto dto)
    {
        var article = new AIKnowledgeArticle
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Category = dto.Category,
            Audience = dto.Audience,
            Content = dto.Content,
            Summary = dto.Summary,
            IsAIGenerated = dto.IsAIGenerated,
            GeneratedAt = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow
        };

        var created = await _repository.AddAsync(article);
        return MapToDto(created);
    }

    private KnowledgeArticleDto MapToDto(AIKnowledgeArticle a)
    {
        return new KnowledgeArticleDto(
            a.Id,
            a.Title,
            a.Category,
            a.Audience,
            a.Content,
            a.Summary,
            a.IsAIGenerated,
            a.Disclaimer
        );
    }
}
