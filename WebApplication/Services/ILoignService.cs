using WebApplication.Models;

namespace WebApplication.Services
{
    public interface ILoginService
    {
        Task<LoginResponse> Authinticate(LoginRequest loginRequest);
    }
}
