using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.ArshaHamrah.Areas.Admin.Pages.Colors;

[Authorize(Roles = "Admin,SuperAdmin")]
public class DetailModel : PageModel
{
    private readonly IColorService _colorService;

    public DetailModel(IColorService colorService)
    {
        _colorService = colorService;
    }

    public Color Color { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _colorService.GetById(id);
        if (result.Code == 0)
        {
            Color = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/Colors/Index",
            new {area = "Admin", message = result.Message, code = result.Code.ToString()});
    }
}