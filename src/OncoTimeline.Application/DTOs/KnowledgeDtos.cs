using System;

namespace OncoTimeline.Application.DTOs;

public record KnowledgeArticleDto(
    Guid Id,
    string Title,
    string Category,
    string Audience,
    string Content,
    string Summary,
    bool IsAIGenerated,
    string Disclaimer
);

public record CreateKnowledgeArticleDto(
    string Title,
    string Category,
    string Audience,
    string Content,
    string Summary,
    bool IsAIGenerated
);
