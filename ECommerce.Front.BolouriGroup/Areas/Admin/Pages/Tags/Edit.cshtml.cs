using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.Tags;

public class EditModel : PageModel
{
    private readonly ITagService _tagService;

    public EditModel(ITagService tagService)
    {
        _tagService = tagService;
    }

    [BindProperty] public Tag Tag { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task OnGet(int id)
    {
        var result = await _tagService.GetById(id);
        Tag = result.ReturnData;
    }

    public async Task<IActionResult> OnPost()
    {
        if (Tag.TagText.Contains("@") || Tag.TagText.Contains("&") || Tag.TagText.Contains("*") ||
            Tag.TagText.Contains("/") || Tag.TagText.Contains("\\"))
        {
            Message = "از علامت های @ & * / \\ استفاده نکنید";
            Code = "Error";
            return Page();
        }
        if (ModelState.IsValid)
        {
            var result = await _tagService.Edit(Tag);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/Tags/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        return Page();
    }
}