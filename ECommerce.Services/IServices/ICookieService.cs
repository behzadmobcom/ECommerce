using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Http;

namespace Services.IServices
{
    public interface ICookieService
    {
        void SetCookie(HttpContext context,CookieData data);
        List<CookieData> GetCookie(HttpContext context, string key,bool startWith = true);
        void Remove(HttpContext context, CookieData data);
        Task SetToken(string token);
        string GetToken();
        Task LogOut();
        LoginViewModel GetCurrentUser();
    }
}
