using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages;

public class OldLoginModel : PageModel
{
    private readonly IUserService _userService;

    public OldLoginModel(IUserService userService)
    {
        _userService = userService;
    }

    [BindProperty] public RegisterViewModel RegisterViewModel { get; set; }

    [BindProperty] public LoginViewModel LoginViewModel { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    [TempData] public string ReturnUrl { get; set; }

    public void OnGet(string returnUrl = null)
    {
        if (string.IsNullOrEmpty(returnUrl)) returnUrl = "/";
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostSubmit()
    {
        var s = TempData["ReturnUrl"];
        //ReturnUrl = "/Shop/Coffee/shop/equipment/Hot.bar/Coffee.makers";
        var result = await _userService.Login(LoginViewModel);
        if (result.Code == 0)
            return RedirectToPage("/Index");

        Message = result.Message;
        Code = result.Code.ToString();

        return Page();
    }

    public async Task<IActionResult> OnPostRegister()
    {
        if (!ModelState.IsValid) return Page();
        var result = await _userService.Register(RegisterViewModel);
        Message = result.Message;
        Code = result.Code.ToString();
        if (result.Code == 0) return RedirectToPage("/Index");
        return Page();
    }

    public async Task<JsonResult> OnGetSecondsLeft(string username)
    {
        ServiceResult<int?> checkUsernameResult = await CheckUsername(username);
        return new JsonResult(checkUsernameResult);
    }

    private async Task<ServiceResult<int?>> CheckUsername(string username)
    {
        ServiceResult<int?> checkUsernameResult = await _userService.GetSecondsLeftConfirmCodeExpire(username);

        if (checkUsernameResult.Code == ServiceCode.Error)
        {
            return new ServiceResult<int?>
            {
                Message = "نام کاربری موجود نمی باشد",
                Code = ServiceCode.Error
            };
        }

        if (checkUsernameResult.ReturnData > 0)
        {
            return new ServiceResult<int?>
            {
                Message = $"{checkUsernameResult.ReturnData}ثانیه باقی مانده",
                Code = ServiceCode.Info,
                ReturnData = checkUsernameResult.ReturnData
            };
        }

        return new ServiceResult<int?>
        {
            Message = $"کاربر موجود و امکان ارسال پیامک وجود دارد",
            Code = ServiceCode.Success
        };
    }

    public async Task<IActionResult> OnGetSendSms(string username)
    {
        ServiceResult<int?> checkUsernameResult = await CheckUsername(username);
        if (checkUsernameResult.Code != ServiceCode.Success)
        {
            return Page();
        }

        Random randomCode = new Random();
        int code = randomCode.Next(100000000);
        if (code < 10000000) code = code + 10000000;
        ResponseVerifySmsIrViewModel smsResponsModel = await _userService.SendAuthenticationSms(username, code.ToString());
        if (smsResponsModel.Status != 1)
        {
            Message = smsResponsModel.Message;
            Code = "Error";
            return Page();
        }
        var result = await _userService.SetConfirmCodeByUsername(username, code.ToString());
        if (!result.ReturnData)
        {
            Message = "نام کاربری صحیح نمی باشد";
            Code = "Error";
            return Page();
        }

        Message = "130 sec";
        Code = "Info";
        return Page();
    }

    public async Task<JsonResult> OnGetUserLoginSubmit(string username, string password)
    {
        LoginViewModel _loginViewModel = new LoginViewModel();
        _loginViewModel.Username = username;
        _loginViewModel.Password = password;
        ServiceResult<LoginViewModel?> result = await _userService.Login(_loginViewModel);       
        return new JsonResult(result);
    }

}