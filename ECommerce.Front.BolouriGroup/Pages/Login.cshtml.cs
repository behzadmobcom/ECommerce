using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages;

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

    public async Task<IActionResult> OnPostSendSms()
    {
        try
        {
            var SecondsLeftConfirmCodeExpire = await _userService.GetSecondsLeftConfirmCodeExpire(LoginViewModel.Username);
            if (SecondsLeftConfirmCodeExpire.Code == ServiceCode.Error)
            {
                Message = "نام کاربری موجود نمی باشد";
                Code = "Error";
                return Page();
            }
            if (SecondsLeftConfirmCodeExpire.ReturnData > 0)
            {
                Message = $"{SecondsLeftConfirmCodeExpire.ReturnData}ثانیه باقی مانده";
                Code = "Info";
                return Page();
            }
            Random randomCode = new Random();
            int code = randomCode.Next(100000000);
            if (code < 10000000) code = code + 10000000;
            SmsIr smsResponsModel = await _userService.SendAuthenticationSms(LoginViewModel.Username, code);
            if (smsResponsModel.Status != 1)
            {
                Message = smsResponsModel.Message;
                Code = "Error";
                return Page();
            }
            var result = await _userService.SetConfirmCodeByUsername(LoginViewModel.Username, code, DateTime.Now.AddSeconds(130));
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
        catch (Exception ex)
        {
            Message = ex.Message;
            Code = "Error";
            return Page();
        }
    }
        

}