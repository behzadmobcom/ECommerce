using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace ECommerce.Services.Services;

public class UserService : EntityService<User>, IUserService
{
    private const string Url = "api/Users";
    private readonly ICookieService _cookieService;
    private readonly IHttpService _http;
    private readonly SmsIrSettings _smsSettings;
    private readonly AdminInfoSetting _adminInfo;
    

    public UserService(IHttpService http, ICookieService cookieService, IOptions<SmsIrSettings> SmsIroptions, IOptions<AdminInfoSetting> CustomerInfoOption) : base(http)
    {
        _http = http;
        _cookieService = cookieService;
        _smsSettings = SmsIroptions.Value;
        _adminInfo = CustomerInfoOption.Value;
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
        ApiResult<object> result = await _http.PostAsync(Url, registerViewModel, "Register");

        if (result.Code != 0 || result.Status != 200) return new ServiceResult
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

    public async Task<ServiceResult> ChangePassword(string oldPass, string newPass, string newConPass)
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

    public async Task<ServiceResult> ChangeForgotPassword(ResetForgotPasswordViewModel resetForgotPasswordViewModel)
    {
        if (!resetForgotPasswordViewModel.Password.Equals(resetForgotPasswordViewModel.ConPass)) return new ServiceResult
        {
            Code = ServiceCode.Success,
            Message = "پسوردها مطابقت ندارند"
        };
        var result = await _http.PostAsync(Url, resetForgotPasswordViewModel, "ResetForgotPassword");

        return new ServiceResult
        {
            Code = ServiceCode.Success,
            Message = result.GetBody()
        };
    }

    public async Task<ServiceResult> ForgotPassword(string email)
    {
        var forgotPasswordViewModel = new ForgotPasswordViewModel { EmailOrPhoneNumber = email };
        var result = await _http.PostAsync(Url, forgotPasswordViewModel, "ForgotPassword");

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

    public async Task<ServiceResult<User>> GetById(int id)
    {
        var result = await _http.GetAsync<User>(Url, $"GetById?id={id}");
        return Return(result);
    }
    public async Task<ServiceResult> Delete(int id)
    {
        var result = await Delete(Url, id);
        return Return(result);
    }
    public async Task<ServiceResult> Edit(User user)
    {
        var result = await _http.PutAsync(Url, user);
        return Return(result);
    }

    public async Task SendInvocieEmailToAdminEmailAddresses(string invoice)
    {
        try
        {
            SmtpClient client = new SmtpClient(_adminInfo.SmtpServer, _adminInfo.Port);            
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_adminInfo.MasterEmail, _adminInfo.MasterEmailPassword);
            client.EnableSsl = true;

            MailMessage message = new MailMessage();
            message.From = new MailAddress(_adminInfo.MasterEmail);
            foreach (var emailTo in _adminInfo.Emails)
            {
                message.To.Add(emailTo);
            }
            message.Subject = "فاکتور جدید";
            message.Body = $"فاکتور جدید به شماره {invoice} صادر شد";
            client.Send(message);         
        }
        catch (Exception ex)
        {
           
        }     
    }

    public async Task<List<ResponseVerifySmsIrViewModel>> SendInvocieSmsToAdminNumbers(string invoice)
    {
        List<ResponseVerifySmsIrViewModel> result = new List<ResponseVerifySmsIrViewModel>();
        try
        {
            foreach (var mobile in _adminInfo.MobileNumbers)
            {
                string apiKey = _smsSettings.apikey;
                string apiName = _smsSettings.apiName;
                string url = _smsSettings.url;
                RequestVerifySmsIrViewModel RequestSMSIrViewModel = new RequestVerifySmsIrViewModel();
                RequestVerifySmsIrParameters invoiceParameter = new RequestVerifySmsIrParameters();
                invoiceParameter.Name = "INVOICE";
                invoiceParameter.Value = invoice;
                RequestVerifySmsIrParameters dateParameter = new RequestVerifySmsIrParameters();
                dateParameter.Name = "DATE";
                dateParameter.Value = DateTime.UtcNow + "";
                RequestSMSIrViewModel.Parameters = new RequestVerifySmsIrParameters[] { invoiceParameter, dateParameter };
                RequestSMSIrViewModel.TemplateId = _smsSettings.invoiceTemplateId;
                RequestSMSIrViewModel.Mobile = mobile;
                var smsResponse = await _http.PostAsyncWithApiKeyByRequestModel<RequestVerifySmsIrViewModel, ResponseVerifySmsIrViewModel>(apiName, apiKey, RequestSMSIrViewModel, url);
                result.Add(smsResponse);
                //Task.Delay(1000).Wait();
            }              
            return result;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<ResponseVerifySmsIrViewModel> SendAuthenticationSms(string? mobile , string code)
    {
        string apiKey = _smsSettings.apikey;
        string apiName = _smsSettings.apiName;
        string url = _smsSettings.url;
        RequestVerifySmsIrViewModel RequestSMSIrViewModel = new RequestVerifySmsIrViewModel();
        RequestVerifySmsIrParameters RequestVerifySmsIrParameter = new RequestVerifySmsIrParameters();
        RequestVerifySmsIrParameter.Name = "CODE";
        RequestVerifySmsIrParameter.Value = code;
        RequestSMSIrViewModel.Parameters = new RequestVerifySmsIrParameters[] { RequestVerifySmsIrParameter };
        RequestSMSIrViewModel.TemplateId = _smsSettings.authenticationTemplateId;
        RequestSMSIrViewModel.Mobile = mobile;
        var result = await _http.PostAsyncWithApiKeyByRequestModel<RequestVerifySmsIrViewModel, ResponseVerifySmsIrViewModel>(apiName, apiKey, RequestSMSIrViewModel, url);
        
        return result;
    }

    public async Task<ServiceResult<bool>> SetConfirmCodeByUsername(string username, string confirmCode)
    {
        var result = await _http.GetAsync<bool>(Url, $"SetConfirmCodeByUsername?username={username}" +
                                                     $"&confirmCode={confirmCode}");
        return Return(result);
    }

    public async Task<ServiceResult<int?>> GetSecondsLeftConfirmCodeExpire(string username)
    {
        var result = await _http.GetAsync<int?>(Url, $"GetSecondsLeftConfirmCodeExpire?username={username}");
        return Return(result);
    }

    public async Task<bool> GetVerificationByNationalId(string nationalId)
    {
        if (nationalId ==null || nationalId.Length!=10) return false;
        int[] nationalIdArray = new int[10];
        int sum = 0;
        for (int i = 0; i < nationalId.Length; i++)
        {
            nationalIdArray[i]= int.Parse(nationalId[i].ToString());
            if (i<9)
            {
                sum += nationalIdArray[i] * (10-i);
            }
        }
        int remainder = sum % 11;
        if (remainder <2 && nationalIdArray[9] == remainder || remainder >=2 && nationalIdArray[9] == 11 - remainder)
        {
            return true;
        }

        return false;
    }
    
}