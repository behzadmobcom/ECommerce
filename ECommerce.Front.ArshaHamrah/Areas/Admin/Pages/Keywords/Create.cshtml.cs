using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.ArshaHamrah.Areas.Admin.Pages.Keywords;

[Authorize(Roles = "Admin,SuperAdmin")]
public class CreateModel : PageModel
{
    private readonly IKeywordService _keywordService;

    public CreateModel(IKeywordService keywordService)
    {
        _keywordService = keywordService;
    }

    [BindProperty] public Keyword Keyword { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var result = await _keywordService.Add(Keyword);
            if (result.Code == 0)
                return RedirectToPage("/Keywords/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        return Page();
    }
}