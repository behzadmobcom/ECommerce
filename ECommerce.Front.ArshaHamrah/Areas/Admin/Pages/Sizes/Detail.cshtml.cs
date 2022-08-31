using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ArshaHamrah.Areas.Admin.Pages.Sizes;

[Authorize(Roles = "Admin,SuperAdmin")]
public class DetailModel : PageModel
{
    private readonly ISizeService _sizeService;

    public DetailModel(ISizeService sizeService)
    {
        _sizeService = sizeService;
    }

    public Size Size { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _sizeService.GetById(id);
        if (result.Code == 0)
        {
            Size = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/Sizes/Index",
            new {area = "Admin", message = result.Message, code = result.Code.ToString()});
    }
}