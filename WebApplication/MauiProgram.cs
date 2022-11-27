using WebApplication.Services;
using WebApplication.ViewModels.StartUp;
using WebApplication.Views.StartUp;


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
            //Views

            builder.Services.AddSingleton<LoginPage>();

            //View Models
            builder.Services.AddSingleton<LoginPageViewModel>();

            return builder.Build();
        }
    }
}