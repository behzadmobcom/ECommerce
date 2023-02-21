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

    public async Task<IActionResult> OnGetSendSms(string username)
    {
        SendSmsOutput SendSmsOutput = new SendSmsOutput();
        SendSmsOutput.Seconds = 0;
        try
        {
            var SecondsLeftConfirmCodeExpire = await _userService.GetSecondsLeftConfirmCodeExpire(username);
            if (SecondsLeftConfirmCodeExpire.Code == ServiceCode.Error)
            {
                SendSmsOutput.Message = "نام کاربری موجود نمی باشد";
                SendSmsOutput.Status = "error";
                SendSmsOutput.Title = "خطا";
                return new JsonResult(SendSmsOutput);
            }
            if (SecondsLeftConfirmCodeExpire.ReturnData > 0)
            {
                SendSmsOutput.Message =  SecondsLeftConfirmCodeExpire.ReturnData + " ثانیه دیگر امتحان کنید ";
                SendSmsOutput.Seconds = SecondsLeftConfirmCodeExpire.ReturnData;
                SendSmsOutput.Status = "warning";
                SendSmsOutput.Title = "اخطار";
                return new JsonResult(SendSmsOutput);
            }
            Random randomCode = new Random();
            int code = randomCode.Next(100000000);
            if (code < 10000000) code = code + 10000000;
            SmsIr smsResponsModel = await _userService.SendAuthenticationSms(username, code);
            if (smsResponsModel.Status != 1)
            {
                SendSmsOutput.Message = smsResponsModel.Message;
                SendSmsOutput.Status = "error";
                SendSmsOutput.Title = "خطا";
                return new JsonResult(SendSmsOutput);
            }
            var result = await _userService.SetConfirmCodeByUsername(username, code);
            if (!result.ReturnData)
            {
                SendSmsOutput.Message = "نام کاربری صحیح نمی باشد";
                SendSmsOutput.Status = "error";
                SendSmsOutput.Title = "خطا";
                return new JsonResult(SendSmsOutput);
            }

            SendSmsOutput.Message = "پیامک حاوی رمز موقت به شماره شما ارسال شد";
            SendSmsOutput.Status = "success";
            SendSmsOutput.Title = "پیامک ارسال شد";
            SendSmsOutput.Seconds = 130;
            return new JsonResult(SendSmsOutput);
        }
        catch (Exception ex)
        {
            SendSmsOutput.Message = "خطای غیر منتظره";
            SendSmsOutput.Status = "error";
            SendSmsOutput.Title = "خطا";
            return new JsonResult(SendSmsOutput);
        }
    }
}
