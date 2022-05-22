using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;

namespace Bolouri.Areas.Admin.Pages.Units;

public class DetailModel : PageModel
{
    private readonly IUnitService _unitService;

    public DetailModel(IUnitService unitService)
    {
        _unitService = unitService;
    }

    public Unit Unit { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _unitService.GetById(id);
        if (result.Code == 0)
        {
            Unit = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/Units/Index",
            new {area = "Admin", message = result.Message, code = result.Code.ToString()});
    }
}