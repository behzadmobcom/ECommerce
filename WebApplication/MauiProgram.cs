using Ecommerce.Entities.Helper;
using WebApplication.Services;
using WebApplication.ViewModels.StartUp;
using WebApplication.Views.StartUp;
using WebApplication.Models;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using ECommerce.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Reflection;
using Android.Telephony.Data;



namespace WebApplication
{
    public static class MauiProgram
    {


        public static MauiApp CreateMauiApp()
        {


            var builder = MauiApp.CreateBuilder();
            //var a = Assembly.GetExecutingAssembly();
            //var stream = a.GetManifestResourceStream("WebApplication.appsettings.json");

            //var config = new ConfigurationBuilder()
            //.AddJsonStream(stream)
            //.Build();

            // builder.Configuration.AddConfiguration(config);
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }) ;

            builder.Services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
            builder.Services.AddScoped<IHttpService, HttpService>();
            //builder.Services.AddSingleton<ILoginService, LoginService>();
            builder.Services.AddScoped<ICookieService, CookieService>();

            builder.Services.AddScoped<IUserService, UserService>();
            //Views

            builder.Services.AddSingleton<LoginPage>();

            //View Models
            builder.Services.AddScoped<LoginPageViewModel>();

            builder.Services.AddScoped<LoginViewModel>();
            builder.Services.AddScoped<SiteSettings>();
            //builder.Services.AddSingleton<FrontSettings>();

            var _frontSetting = builder.Configuration.GetSection(nameof(FrontSetting)).Get<FrontSetting>();

            builder.Services.AddTransient(_ => new HttpClient { BaseAddress = new Uri(_frontSetting.BaseAddress) });
           builder.Services.AddHttpContextAccessor();

            return builder.Build();
        }
    }
}