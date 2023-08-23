using Ecommerce.Entities;
using ECommerce.Services.IServices;
using ECommerce.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.States;

public class DetailModel : PageModel
{
    private readonly IStateService _stateService;

    public DetailModel(IStateService stateService)
    {
        _stateService = stateService;
    }

    public State State { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _stateService.GetById(id);
        if (result.Code == 0)
        {
            State = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/States/Index",
            new {area = "Admin", message = result.Message, code = result.Code.ToString()});
    }
}