using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.Keywords;

public class DetailModel : PageModel
{
    private readonly IKeywordService _keywordService;

    public DetailModel(IKeywordService keywordService)
    {
        _keywordService = keywordService;
    }

    public Keyword Keyword { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _keywordService.GetById(id);
        if (result.Code == 0)
        {
            Keyword = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/Keywords/Index",
            new {area = "Admin", message = result.Message, code = result.Code.ToString()});
    }
}