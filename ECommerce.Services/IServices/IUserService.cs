using Entities.ViewModel;

using System.Threading.Tasks;
using Entities.Helper;

namespace Services.IServices
{
    public interface IUserService
    {
        Task<ServiceResult> Logout();
        Task<ServiceResult<LoginViewModel>> Login(LoginViewModel loginViewModel);
        Task<ServiceResult> Register(RegisterViewModel registerViewModel);
    }
}
