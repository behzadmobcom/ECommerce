using Ecommerce.Entities.Helper;
using WebApplication.Services;
using WebApplication.ViewModels.StartUp;
using WebApplication.Views.StartUp;
using WebApplication.Models;
using FrontSetting = Ecommerce.Entities.Helper.FrontSetting;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using ECommerce.Services.Services;
using Microsoft.Extensions.Configuration;
using Android.Content.Res;

namespace WebApplication
{
    public static class MauiProgram
    {


        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<ILoginService, LoginService>();
            builder.Services.AddSingleton<IUserService, UserService>();
            //Views

            builder.Services.AddSingleton<LoginPage>();

            //View Models
            builder.Services.AddSingleton<LoginPageViewModel>();

            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<SiteSettings>();
            //builder.Services.AddSingleton<FrontSettings>();

            var _frontSetting = builder.Configuration.GetSection(nameof(FrontSetting)).Get<FrontSetting>();

            builder.Services.AddTransient(_ => new HttpClient { BaseAddress = new Uri(_frontSetting.BaseAddress) });
           builder.Services.AddHttpContextAccessor();

            return builder.Build();
        }
    }
}