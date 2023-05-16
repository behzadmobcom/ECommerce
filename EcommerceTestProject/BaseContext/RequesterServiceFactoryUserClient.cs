using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ControllersTests.BaseContext
{
    public class RequesterServiceFactoryUserClient : IRequesterServiceFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RequesterServiceFactoryUserClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IRequesterService Create(string httpClientName)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient(httpClientName);
            return new RequesterServiceUserClient(httpClient);
        }
    }
}
