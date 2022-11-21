using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Services.IServices;

public interface ICookieService
{
    void SetCookie(HttpContext context, CookieData data);
    List<CookieData> GetCookie(HttpContext context, string key, bool startWith = true);
    void Remove(HttpContext context, CookieData data);
    Task SetToken(string token);
    string GetToken();
    Task LogOut();
    LoginViewModel GetCurrentUser();
}