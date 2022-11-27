using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
//using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.ViewModels.StartUp
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _userName;
        [ObservableProperty]
        private string _password;

        private readonly ILoginService _loginService;
        public LoginPageViewModel(ILoginService loginService)
        {
            _loginService = loginService;
        }
        #region Commands
        [ICommand]
        async void Login()
        {
            var response = await _loginService.Authinticate(new LoginRequest
            {
                UserName = UserName,
                Password = Password
            });
            if (response != null)
            {
                // await AppShell.Current.DisplayAlert("Valid username","valid username","Ok");

                await Launcher.Default.OpenAsync("https://boloorico.com");

                //Saving Cookies
                CookieContainer cookieContainer = new CookieContainer();
                Uri uri = new Uri("https://boloorico.com", UriKind.RelativeOrAbsolute);

                Cookie cookie = new Cookie
                {
                    Name = "DotNetMAUICookie",
                    Expires = DateTime.Now.AddDays(1),
                    Value = "My cookie",
                    Domain = uri.Host,
                    Path = "/"
                };
                cookieContainer.Add(uri, cookie);
                WebView webView = new WebView
                {
                    Source = "https://boloorico.com"
                };

                webView.Cookies = cookieContainer;
                webView.Source = new UrlWebViewSource { Url = uri.ToString() };

                // await SecureStorage.SetAsync(nameof(App.RuturnData), response.ReturnData);
                //var tokenDetails=await SecureStorage.GetAsync(nameof(App.RuturnData));

            }
            else
            {
                // await AppShell.Current.DisplayAlert("inValid username", "invalid username", "Ok");
                await AppShell.Current.DisplayAlert("Invalid User Name Or Password", "نام کاربری یا کلمه عبور صحیح نمی باشد", "Ok");
            }
            // await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
        }
        #endregion



    }
}
