using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.ControllersTests.BaseContext
{
    public static class IServiceCollectionExtensions
    {
        public static void AddWebApiClientSdk(this IServiceCollection services)
        {
            services.AddTransient<MachineAuthenticationDelegatingHandler>();
            services.AddScoped<IRequesterServiceFactory, RequesterServiceFactory>();
            AddWebApiClientSdkDependencies(services);
        }

        public static void AddWebApiClientSdkDependencies(this IServiceCollection services)
        {
            services.AddTransient<IQueryStringConverter, QueryStringConverter>();
        }

        public static void AddHttpClient(this IServiceCollection services, string httpClientName, string baseUrl)
        {
            services.AddHttpClient(
                    httpClientName,
                    c => c.BaseAddress = new Uri(baseUrl))
                .AddHttpMessageHandler<MachineAuthenticationDelegatingHandler>();
        }
    }
}
