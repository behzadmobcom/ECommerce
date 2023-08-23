using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.States;

public class EditModel : PageModel
{
    private readonly IStateService _stateService;

    public EditModel(IStateService stateService)
    {
        _stateService = stateService;
    }

    [BindProperty] public State State { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task OnGet(int id)
    {
        var result = await _stateService.GetById(id);
        State = result.ReturnData;
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var result = await _stateService.Edit(State);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/States/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        return Page();
    }
}