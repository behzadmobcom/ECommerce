using Ecommerce.Entities;
using ECommerce.Services.IServices;
using ECommerce.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.Cities;

public class DeleteModel : PageModel
{
    private readonly ICityService _cityService;
    private readonly IStateService _stateService;
    public string StateName;

    public DeleteModel(ICityService cityService, IStateService stateService)
    {
        _cityService = cityService;
        _stateService = stateService;
    }

    public City City { get; set; }
    public SelectList StateCity { get; set; }
    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _cityService.GetById(id);
        var stateCity = (await _stateService.GetAll()).ReturnData;
        if (result.Code == 0)
        {
            City = result.ReturnData;
            StateName = stateCity.First(x => x.Id == City.StateId).Name;
            return Page();
        }

        return RedirectToPage("/Cities/Index",
            new { area = "Admin", message = result.Message, code = result.Code.ToString() });
    }

    public async Task<IActionResult> OnPost(int id)
    {
        if (ModelState.IsValid)
        {
            var result = await _cityService.Delete(id);
            return RedirectToPage("/Cities/Index",
                new { area = "Admin", message = result.Message, code = result.Code.ToString() });
        }

        return Page();
    }
}