namespace ECommerce.ControllersTests.BaseContext
{
    public class RequesterServiceFactory : IRequesterServiceFactory
    {
        private readonly IAuthorizationContextProvider _authorizationContextProvider;
        private readonly IHttpClientFactory _httpClientFactory;

        public RequesterServiceFactory(
            IHttpClientFactory httpClientFactory,
            IAuthorizationContextProvider authorizationContextProvider)
        {
            _httpClientFactory = httpClientFactory;
            _authorizationContextProvider = authorizationContextProvider;
        }

        public IRequesterService Create(string httpClientName)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient(httpClientName);
            return new RequesterService(httpClient, _authorizationContextProvider);
        }
    }
}
