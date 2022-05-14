using Entities.Helper;
using Entities.ViewModel;
using Services.IServices;

namespace Services.Services;

public class UserService : IUserService
{
    private const string Url = "api/Users";
    private readonly ICookieService _cookieService;
    private readonly IHttpService _http;

    public UserService(IHttpService http, ICookieService cookieService)
    {
        _http = http;
        _cookieService = cookieService;
    }


    public async Task<ServiceResult> Logout()
    {
        var result = await _http.GetAsync<bool>(Url, "LogoutUser");
        if (result.Code == ResultCode.Success)
            return new ServiceResult
            {
                Code = ServiceCode.Success,
                Message = result.Messages?.FirstOrDefault()
            };
        return new ServiceResult
        {
            Code = ServiceCode.Error,
            Message = result.GetBody()
        };
    }

    public async Task<ServiceResult<LoginViewModel>> Login(LoginViewModel loginViewModel)
    {
        var result = await _http.PostAsync<LoginViewModel, string>(Url, loginViewModel, "Login");
        if (result.Code == 0)
        {
            await _cookieService.SetToken(result.ReturnData);

            var resultCurrentUser = _cookieService.GetCurrentUser();


            return new ServiceResult<LoginViewModel>
            {
                Code = ServiceCode.Success,
                Message = "با موفقیت وارد شدید",
                ReturnData = resultCurrentUser
            };
        }

        if (result.Code == ResultCode.NotFound)
            return new ServiceResult<LoginViewModel>
            {
                Code = ServiceCode.Warning,
                Message = "نام کاربر یا کلمه عبور اشتباه می باشد"
            };
        return new ServiceResult<LoginViewModel>
        {
            Code = ServiceCode.Error,
            Message = "مشکل در ارتباط با سرور"
        };
    }

    public async Task<ServiceResult> Register(RegisterViewModel registerViewModel)
    {
        var result = await _http.PostAsync(Url, registerViewModel, "Register");

        if (result.Code != 0) return new ServiceResult {Code = ServiceCode.Error};

        var loginViewModel = new LoginViewModel
        {
            Username = registerViewModel.Username,
            Password = registerViewModel.Password,
            RememberMe = true
        };
        await Login(loginViewModel);

        return new ServiceResult
        {
            Code = ServiceCode.Success,
            Message = "ثبت نام با موفقیت انجام شد"
        };
    }
}