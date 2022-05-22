using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;

namespace Bolouri.Areas.Admin.Pages.Keywords;

public class EditModel : PageModel
{
    private readonly IKeywordService _keywordService;

    public EditModel(IKeywordService keywordService)
    {
        _keywordService = keywordService;
    }

    [BindProperty] public Keyword Keyword { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task OnGet(int id)
    {
        var result = await _keywordService.GetById(id);
        Keyword = result.ReturnData;
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var result = await _keywordService.Edit(Keyword);
            Message = result.Message;
            Code = result.Code.ToString();
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