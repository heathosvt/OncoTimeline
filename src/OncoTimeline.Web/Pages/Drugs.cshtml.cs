using Microsoft.AspNetCore.Mvc.RazorPages;
using OncoTimeline.Application.Services;
using OncoTimeline.Application.DTOs;

namespace OncoTimeline.Web.Pages;

public class DrugsModel : PageModel
{
    private readonly DrugService _drugService;

    public List<DrugDetailDto> Drugs { get; set; } = new();

    public DrugsModel(DrugService drugService)
    {
        _drugService = drugService;
    }

    public async Task OnGetAsync()
    {
        var drugs = await _drugService.GetAllDrugsAsync();
        Drugs = drugs.ToList();
    }
}
