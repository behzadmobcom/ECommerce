using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.ArshaHamrah.Areas.Admin.Pages.Keywords;

[Authorize(Roles = "Admin,SuperAdmin")]
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