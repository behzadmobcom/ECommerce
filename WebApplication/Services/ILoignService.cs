using WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Services
{
    public interface ILoginService
    {
        Task<LoginResponse> Authinticate(LoginRequest loginRequest);
    }
}
