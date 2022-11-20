using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.ArshaHamrah.Areas.Admin.Pages.Tags;

[Authorize(Roles = "Admin,SuperAdmin")]
public class DetailModel : PageModel
{
    private readonly ITagService _tagService;

    public DetailModel(ITagService tagService)
    {
        _tagService = tagService;
    }

    public Tag Tag { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _tagService.GetById(id);
        if (result.Code == 0)
        {
            Tag = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/Tags/Index",
            new {area = "Admin", message = result.Message, code = result.Code.ToString()});
    }
}