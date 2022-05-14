using Entities.Helper;
using Entities.ViewModel;

namespace Services.IServices;

public interface IUserService
{
    Task<ServiceResult> Logout();
    Task<ServiceResult<LoginViewModel>> Login(LoginViewModel loginViewModel);
    Task<ServiceResult> Register(RegisterViewModel registerViewModel);
}