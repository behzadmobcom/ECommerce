using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.Admin.Pages;

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

    public async Task<IActionResult> OnGetSendRegisterSms(string username)
    {
        ResponseVerifySmsIrViewModel smsResponsModel = new ResponseVerifySmsIrViewModel();
        IActionResult codeResult = await OnGetGenerateCode(username);
        int code = (int)(codeResult as JsonResult).Value;
        if (code == 0)
        {
            smsResponsModel.Message = "فرمت شماره موبایل صحیح نمی باشد";
            return new JsonResult(smsResponsModel);
        }
        smsResponsModel = await _userService.SendAuthenticationSms(username, code.ToString());
        smsResponsModel.Message = "خطای غیر منتظره. امکان ارسال پیامک وجود ندارد";
        if (smsResponsModel.Status == 1) smsResponsModel.Message = "پیامک با موفقیت ارسال شد";
        if (smsResponsModel.Status == 104) smsResponsModel.Message = "شماره موبایل وارد شده صحیح نمی باشد";
        return new JsonResult(smsResponsModel);
    }

    public async Task<IActionResult> OnGetGenerateCode(string mobile)
    { 
        if (mobile == null) return new JsonResult(0);
        int number;
        if (mobile.Length != 11 & mobile.Length != 10) return new JsonResult(0);
        if (mobile.Substring(0, 1) != "0") mobile = "0" + mobile;
        if (mobile.Substring(0, 2) != "09") return new JsonResult(0);
        if (!int.TryParse(mobile.Substring(1, 9), out number)) return new JsonResult(0);
        var result = getSumResult(mobile.Substring(10, 1), mobile.Substring(4, 1));
        result = result + getSumResult(mobile.Substring(9, 1), mobile.Substring(5, 1));
        result = result + getSumResult(mobile.Substring(8, 1), mobile.Substring(6, 1));
        result = result + getSumResult(mobile.Substring(7, 1), mobile.Substring(3, 1));
        int.TryParse(result, out number);
        return new JsonResult(number);
    }
    private string getSumResult(string num1, string num2)
    {
        int number1, number2;
        int.TryParse(num1, out number1);
        int.TryParse(num2, out number2);
        int sum;
        do
        {
            sum = number1 + number2;
            if (sum > 10)
            {
                number1 = sum - 10;
                number2 = number1 > 4 ? 1 : 0;
            }
        } while (sum > 10);
        return sum + "";
    }

    public async Task<JsonResult> OnGetUserLoginSubmit(string username, string password)
    {
        LoginViewModel _loginViewModel = new() {Username = username, Password = password};        
        ServiceResult<LoginViewModel?> result = await _userService.Login(_loginViewModel);
        return new JsonResult(result);
    }

    public async Task<IActionResult> OnGetSendConfirmSmsToExistUser(string username)
    {
        ResponseVerifySmsIrViewModel smsResponsModel = new ResponseVerifySmsIrViewModel();
        try {            
            Random random = new Random();
            int code = random.Next(10000000, 99999999);
            var result = await _userService.SetConfirmCodeByUsername(username, code + "");
            if (result == null) return new JsonResult(smsResponsModel);
            if (result.ReturnData == false) return new JsonResult(smsResponsModel);
            smsResponsModel = await _userService.SendAuthenticationSms(username, code.ToString());
            smsResponsModel.Message = "خطای غیر منتظره. امکان ارسال پیامک وجود ندارد";
            if (smsResponsModel.Status == 1) smsResponsModel.Message = "پیامک با موفقیت ارسال شد";
            if (smsResponsModel.Status == 104) smsResponsModel.Message = "شماره موبایل وارد شده صحیح نمی باشد";
            return new JsonResult(smsResponsModel);
        }
        catch (Exception ex)
        {
            return new JsonResult(smsResponsModel);
        }
        
    }
}
