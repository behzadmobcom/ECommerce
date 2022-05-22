using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using ECommerce.Services.IServices;

namespace Services.Services;

public class CookieService : ICookieService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CookieService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void SetCookie(HttpContext context, CookieData data)
    {
        var options = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(data.Days)
        };
        //var value = JsonConvert.SerializeObject(data.Value);
        context.Response.Cookies.Append(data.Key, data.Value.ToString(), options);
    }

    public List<CookieData> GetCookie(HttpContext context, string key, bool startWith = true)
    {
        var productIds = new List<CookieData>();
        if (startWith)
        {
            var cookies = context.Request.Cookies;
            if (cookies == null) return default;
            foreach (var cookie in cookies)
            {
                if (!cookie.Key.StartsWith(key))
                    continue;
                productIds.Add(new CookieData(cookie.Key, Convert.ToInt32(cookie.Value)));
            }
        }
        else
        {
            var value = context.Request.Cookies[key];
            productIds.Add(new CookieData(key, Convert.ToInt32(value)));
        }

        //var val = JsonConvert.DeserializeObject<T>(value);
        return productIds;
    }

    public void Remove(HttpContext context, CookieData data)
    {
        foreach (var cookie in context.Request.Cookies)
            if (cookie.Key == data.Key)
            {
                context.Response.Cookies.Append(data.Key, "", new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(-1)
                });
                context.Response.Cookies.Delete(cookie.Key);
            }
    }

    public async Task SetToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        var expired = Convert.ToInt32(jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Expired).Value);

        var claims = new List<Claim>
        {
            new("Token", token),
            new(ClaimTypes.Role, jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Role).Value),
            new(ClaimTypes.Name, jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Name).Value)
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            //AllowRefresh = <bool>,
            // Refreshing the authentication session should be allowed.

            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(expired),
            // The time at which the authentication ticket expires. A 
            // value set here overrides the ExpireTimeSpan option of 
            // CookieAuthenticationOptions set with AddCookie.

            IsPersistent = true

            //IssuedUtc = <DateTimeOffset>,
            // The time at which the authentication ticket was issued.

            //RedirectUri = <string>
            // The full path or absolute URI to be used as an http 
            // redirect response value.
        };
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await _httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsPrincipal),
            authProperties);
    }

    public string GetToken()
    {
        return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value;
    }

    public async Task LogOut()
    {
        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public LoginViewModel GetCurrentUser()
    {
        try
        {
            var token = GetToken();
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var loginViewModel = new LoginViewModel
            {
                Id = Convert.ToInt32(jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier)
                    .Value),
                Username = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Name).Value,
                FullName = jwtSecurityToken.Claims.First(claim => claim.Type == "FullName").Value,
                AuthName = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Role).Value,
                IsActive = Convert.ToBoolean(jwtSecurityToken.Claims.First(claim => claim.Type == "IsActive").Value),
                IsColleague =
                    Convert.ToBoolean(jwtSecurityToken.Claims.First(claim => claim.Type == "IsColleague").Value)
            };
            return loginViewModel;
        }
        catch (Exception exception)
        {
            return new LoginViewModel
            {
                Id = 0
            };
        }
    }
}