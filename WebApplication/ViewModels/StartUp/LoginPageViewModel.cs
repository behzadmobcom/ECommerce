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
using ECommerce.Services.IServices;
using Ecommerce.Entities.ViewModel;
using Ecommerce.Entities.Helper;

namespace WebApplication.ViewModels.StartUp
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _userName;
        [ObservableProperty]
        private string _password;

        private readonly IUserService _userService;
        //private readonly ILoginService _loginService;

        public LoginPageViewModel(IUserService userService)
        {
            _userService = userService;
        }
        //public LoginPageViewModel(ILoginService loginService)
        //{
        //    _loginService = loginService;
        //}
        #region Commands
        [ICommand]
        async void LoginUser()
        {
            var result = await _userService.Login(new LoginViewModel
            {
                Username = UserName,
                Password = Password
            });
            //if (response != null)
             if (result.Code == ServiceCode.Success)
                {
                // await AppShell.Current.DisplayAlert("Valid username","valid username","Ok");

                await Launcher.Default.OpenAsync("https://boloorico.com");

                                
                // await SecureStorage.SetAsync(nameof(App.RuturnData), response.ReturnData);
                //var tokenDetails=await SecureStorage.GetAsync(nameof(App.RuturnData));

            }
            else
            {
                // await AppShell.Current.DisplayAlert("inValid username", "invalid username", "Ok");
                await AppShell.Current.DisplayAlert("Invalid User Name Or Password", "invalid username", "Ok");
            }
            // await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
        }
        #endregion



    }
}
