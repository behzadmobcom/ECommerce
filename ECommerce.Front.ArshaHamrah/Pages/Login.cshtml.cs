using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.ArshaHamrah.Pages;

public class LoginModel : PageModel
{
    private readonly IUserService _userService;

    public LoginModel(IUserService userService)
    {
        _userService = userService;
    }

    [BindProperty] public RegisterViewModel RegisterViewModel { get; set; }

    [BindProperty] public LoginViewModel LoginViewModel { get; set; }
    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    [BindProperty] public string ReturnUrl { get; set; }


    public void OnGet(string returnUrl = null)
    {
        if (string.IsNullOrEmpty(returnUrl)) returnUrl = "/";
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostSubmit()
    {
        var result = await _userService.Login(LoginViewModel);
        if (result.Code == 0)
            return RedirectToPage(ReturnUrl == "/" ? "/Index" : ReturnUrl);

        Message = result.Message;
        Code = result.Code.ToString();

        return Page();
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
        ModelState["Username"].ValidationState = ModelValidationState.Valid;
        ModelState["Password"].ValidationState = ModelValidationState.Valid;
        ModelState["ReturnUrl"].ValidationState = ModelValidationState.Valid;
        if (!ModelState.IsValid) return Page();
        var result = await _userService.Register(RegisterViewModel);
        Message = result.Message;
        Code = result.Code.ToString();
        //if (result.Code == 0) return RedirectToPage("Index");
        return Page();
    }

    public async Task<IActionResult> OnGetForgotPassword(string email) 
    {
        var result = await _userService.ForgotPassword(email);
        return new JsonResult(result);
    }
}