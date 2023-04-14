using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;

namespace ECommerce.Services.IServices;

public interface IUserService
{
    Task<ServiceResult> Logout();
    Task<ServiceResult<LoginViewModel>> Login(LoginViewModel loginViewModel);
    Task<ServiceResult> Register(RegisterViewModel registerViewModel);
    Task<ServiceResult<List<UserListViewModel>>> UserList(string search = "",
     int pageNumber = 0, int pageSize = 10, int userSort = 1, bool? isActive = null, bool? isColleague = null, bool? HasBuying = null);
    Task<ServiceResult<User>> GetUser();
    Task<ServiceResult> ChangePassword(string oldPass, string newPass, string newConPass);
    Task<ServiceResult> Update(User user);
    Task<ServiceResult> ForgotPassword(string email);
    Task<ServiceResult> ChangeForgotPassword(ResetForgotPasswordViewModel resetForgotPasswordViewModel);

    Task<ServiceResult<User>> GetById(int id);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult> Edit(User user);
    Task<ResponseVerifySmsIrViewModel> SendAuthenticationSms(string? mobile, string code);
    Task<ServiceResult<bool>> SetConfirmCodeByUsername(string username, string confirmCode);
    Task<ServiceResult<int?>> GetSecondsLeftConfirmCodeExpire(string username);
    Task<List<ResponseVerifySmsIrViewModel>> SendInvocieSmsToAdminNumbers(string invoice);
    Task<bool> GetVerificationByNationalId(string nationalId);
    Task SendInvocieEmailToAdminEmailAddresses(string invoice);
}