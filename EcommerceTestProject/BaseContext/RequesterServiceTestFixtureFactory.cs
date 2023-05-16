namespace ECommerce.ControllersTests.BaseContext
{
    public class RequesterServiceTestFixtureFactory : IRequesterServiceFactory
    {
        private const string LOCAL_HOST = "http://localhost/api/";
        private readonly HttpClient _httpClient;
        private readonly IAuthorizationContextProvider _authorizationContextProvider;

        public RequesterServiceTestFixtureFactory(
            HttpClient httpClient,
            IAuthorizationContextProvider authorizationContextProvider)
        {
            _httpClient = httpClient;
            _authorizationContextProvider = authorizationContextProvider;
        }

        public IRequesterService Create(string httpClientName)
        {
            _httpClient.BaseAddress = new Uri(LOCAL_HOST);
            return new RequesterService(_httpClient, _authorizationContextProvider);
        }
    }
}
