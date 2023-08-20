using Ecommerce.Entities;
using ECommerce.Services.IServices;
using ECommerce.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.Cities;

public class EditModel : PageModel
{
    private readonly ICityService _cityService;
    private readonly IStateService _stateService;

    public EditModel(ICityService cityService, IStateService stateService)
    {
        _cityService = cityService;
        _stateService = stateService;
    }

    [BindProperty] public City City { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }
    public SelectList StateCity { get; set; }

    public async Task OnGet(int id)
    {
        var result = await _cityService.GetById(id);
        City = result.ReturnData;

        var stateCity = (await _stateService.GetAll()).ReturnData;
        StateCity = new SelectList(stateCity, nameof(State.Id),
            nameof(State.Name));
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var result = await _cityService.Edit(City);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/Cities/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        return Page();
    }
}