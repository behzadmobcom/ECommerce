using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages;

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
        if (!RegisterViewModel.IsRole)
        {
            //          !qa@ws#ed123
            //          !qa@ws#ed123123
            Message = "لطفا ایتدا قوانین و مقررارت را تایید کنید";
            Code = "Error";
            return Page();
        }
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