using Microsoft.AspNetCore.Mvc.RazorPages;
using OncoTimeline.Application.Services;
using OncoTimeline.Application.DTOs;

namespace OncoTimeline.Web.Pages;

public class KnowledgeModel : PageModel
{
    private readonly KnowledgeService _knowledgeService;

    public List<KnowledgeArticleDto> Articles { get; set; } = new();

    public KnowledgeModel(KnowledgeService knowledgeService)
    {
        _knowledgeService = knowledgeService;
    }

    public async Task OnGetAsync()
    {
        var articles = await _knowledgeService.GetAllAsync();
        Articles = articles.ToList();
    }
}
