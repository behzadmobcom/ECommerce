namespace ECommerce.ControllersTests.BaseContext
{
    public interface IRequesterService
    {
        IRequesterService WithAuthorizationHeaders(AuthorizationHeaders authorizationHeaders);
        Task<TResult> GetAsync<TResult>(string partialUrl);
        Task<HttpContent> CustomGetAsync(string partialUrl);
        Task PostAsync(string partialUrl);
        Task PostAsync<TBody>(string partialUrl, TBody body);
        Task<TResult> PostAsync<TBody, TResult>(string partialUrl, TBody body);
        Task<TResult> CustomPostAsync<TResult>(HttpRequestMessage httpRequestMessage);
        Task<TResult> PatchAsync<TBody, TResult>(string partialUrl, TBody body);
        Task DeleteAsync(string partialUrl);
        HttpRequestMessage GetHttpRequestMessage(HttpMethod httpMethod, string partialUrl);
    }
}
