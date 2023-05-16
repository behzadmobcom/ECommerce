using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ControllersTests.BaseContext.Extension
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class IServiceCollectionExtensions
    {
        public static void AddUserClientSdk(this IServiceCollection services)
        {
            services.AddTransient<LoggingHandler>();
            services.AddScoped<IRequesterServiceFactory, RequesterServiceFactoryUserClient>();
            AddUserClientSdkDependencies(services);
        }

        public static void AddUserClientSdkDependencies(this IServiceCollection services)
        {
            services.AddTransient<IQueryStringConverter, QueryStringConverter>();
        }

        public static void AddUserHttpClient(this IServiceCollection services, string httpClientName, string baseUrl)
        {
            services.AddHttpClient(httpClientName, c => c.BaseAddress = new Uri(baseUrl));
        }
    }
}
