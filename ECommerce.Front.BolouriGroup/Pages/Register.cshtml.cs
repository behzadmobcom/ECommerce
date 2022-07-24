using ECommerce.Services.IServices;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bolouri.Pages;

public class RegisterModel : PageModel
{
    private readonly IUserService _userService;

    [BindProperty] public RegisterViewModel RegisterViewModel { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public RegisterModel(IUserService userService)
    {
        _userService = userService;

    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostRegister()
    {
        if (!ModelState.IsValid) return Page();
        var result = await _userService.Register(RegisterViewModel);
        if(result.Code>0)
        {
            Message = result.Message;
            Code = result.Code.ToString();
            return Page();
        }
        return RedirectToPage("index");
    }
}