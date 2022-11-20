using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;

namespace ECommerce.Services.Services;

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

        if (result.Code != 0) return new ServiceResult
        {
            Code = ServiceCode.Error,
            Message = result.GetBody()
        };

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

    public async Task<ServiceResult> Update(User user)
    {
        var result = await _http.PutAsync(Url, user);

        return new ServiceResult
        {
            Code = (ServiceCode)result.Code,
            Message = result.GetBody()
        };
    }

    public async Task<ServiceResult> ChangePassword(string oldPass,string newPass, string newConPass)
    {
        if (!newPass.Equals(newConPass)) return new ServiceResult
        {
            Code = ServiceCode.Success,
            Message = "پسوردها مطابقت ندارند"
        };
        var user = _cookieService.GetCurrentUser();
        var resetPasswordViewModel = new ResetPasswordViewModel
        {
            Password = newPass,
            OldPassword = oldPass,
            Username = user.Username
        };
        var result = await _http.PostAsync(Url, resetPasswordViewModel, "ResetPassword");

        return new ServiceResult
        {
            Code = ServiceCode.Success,
            Message = result.GetBody()
        };
    }

    public async Task<ServiceResult<List<UserListViewModel>>> UserList(string search = "",
     int pageNumber = 0, int pageSize = 10, int userSort = 1, bool? isActive = null, bool? isColleague = null, bool? HasBuying = null)
    {
        //var result = await _http.GetAsync<List<ProductIndexPageViewModel>>(Url, $"NewProducts?count={count}");
        //return Return<List<ProductIndexPageViewModel>>(result);
        var command = "Get?" +
                      $"PaginationParameters.PageNumber={pageNumber}&" +
                      $"PaginationParameters.PageSize={pageSize}&";
        if (!string.IsNullOrEmpty(search)) command += $"PaginationParameters.Search='{search}'&";
        if (isActive != null) command += $"IsActive={isActive}&";
        if (isColleague != null) command += $"IsColleauge={isColleague}&";
        if (HasBuying != null) command += $"HasBuying={HasBuying}&";
        command += $"UserSort={userSort}";
        var result = await _http.GetAsync<List<UserListViewModel>>(Url, command);
        return Return(result);
    }

    public async Task<ServiceResult<User>> GetUser()
    {
        var resultCurrentUser = _cookieService.GetCurrentUser();
        var command = "Get/" + resultCurrentUser.Id;
        var result = await _http.GetAsync<User>(Url, command);
        return Return(result);
    }

    private ServiceResult<TResult> Return<TResult>(ApiResult<TResult> result)
    {
        if (result is { Code: ResultCode.Success })
            return new ServiceResult<TResult>
            {
                PaginationDetails = result.PaginationDetails,
                Code = ServiceCode.Success,
                ReturnData = result.ReturnData,
                Message = result.Messages?.FirstOrDefault()
            };
        var typeOfTResult = Activator.CreateInstance(typeof(TResult));
        return new ServiceResult<TResult>
        {
            Code = ServiceCode.Error,
            Message = result?.GetBody(),
            ReturnData = (TResult)typeOfTResult
        };
    }
}