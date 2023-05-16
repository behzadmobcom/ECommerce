using System.Net;
using System.Net.Http;
namespace ECommerce.ControllersTests.BaseContext
{
    public class RequesterService : IRequesterService
    {
        private readonly IAuthorizationContextProvider _authorizationContextProvider;
        private readonly HttpClient _httpClient;

        private AuthorizationHeaders _authorizationHeaders;

        public RequesterService(HttpClient httpClient, IAuthorizationContextProvider authorizationContextProvider)
        {
            _httpClient = httpClient;
            _authorizationContextProvider = authorizationContextProvider;
        }

        public IRequesterService WithAuthorizationHeaders(AuthorizationHeaders authorizationHeaders)
        {
            _authorizationHeaders = authorizationHeaders;
            return this;
        }

        public async Task<TResult> GetAsync<TResult>(string partialUrl)
        {
            HttpRequestMessage request = GetHttpRequestMessage(HttpMethod.Get, partialUrl);
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return await HandleResponseAsync<TResult>(response);
        }

        public HttpRequestMessage GetHttpRequestMessage(HttpMethod httpMethod, string partialUrl)
        {
            var request = new HttpRequestMessage(httpMethod, partialUrl);
            request = SetAuthorizationHeadersIfAuthorizationContextExists(request);
            OverrideAuthorizationHeadersIfNecessary(request);
            return request;
        }

        private HttpRequestMessage SetAuthorizationHeadersIfAuthorizationContextExists(HttpRequestMessage request)
        {
            try
            {
                AuthorizationContext authorizationContext = _authorizationContextProvider.GetAuthorizationContext();
                SetAuthorizationContextHeaders(request, authorizationContext);
            }
            catch (InvalidOperationException)
            {
            }

            return request;
        }

        private void SetAuthorizationContextHeaders(
            HttpRequestMessage request,
            AuthorizationContext authorizationContext)
        {
            request.Headers.Add(
                "HttpMachineAuthenticationHeaderConstants.ORGANIZATION_ID_KEY",
                authorizationContext.OrganizationId?.ToString());
            request.Headers.Add(
                "HttpMachineAuthenticationHeaderConstants.ORGANIZATIONAL_ROLE_KEY",
                authorizationContext.OrganizationalRole?.ToString());
            request.Headers.Add(
                "HttpMachineAuthenticationHeaderConstants.PERSON_ID_KEY", authorizationContext.PersonId?.ToString());
            request.Headers.Add(
                "HttpMachineAuthenticationHeaderConstants.ACCESS_LEVEL_KEY",
                authorizationContext.AccessLevel.ToString());
        }

        private void OverrideAuthorizationHeadersIfNecessary(HttpRequestMessage request)
        {
            if (_authorizationHeaders != default)
            {
                OverrideOrganizationIdIfNecessary(request);
                OverrideAccessLevel(request);
                _authorizationHeaders = default;
            }
        }

        private void OverrideOrganizationIdIfNecessary(HttpRequestMessage request)
        {
            if (_authorizationHeaders.OrganizationId.HasValue)
            {
                request.Headers.AddOrUpdate(
                    "HttpMachineAuthenticationHeaderConstants.ORGANIZATION_ID_KEY",
                    _authorizationHeaders.OrganizationId.ToString());
            }
        }

        private void OverrideAccessLevel(HttpRequestMessage request)
        {
            request.Headers.AddOrUpdate(
                "HttpMachineAuthenticationHeaderConstants.ACCESS_LEVEL_KEY",
                _authorizationHeaders.AccessLevel.ToString());
        }

        private async Task<TResult> HandleResponseAsync<TResult>(HttpResponseMessage response)
        {
            await ValidateSuccessStatusCodeAsync(response);
            return await JsonContentSerializer.DeserializeAsync<TResult>(response.Content);
        }

        private async Task ValidateSuccessStatusCodeAsync(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw await HandleErrorAsync(response);
            }
        }

        private async Task<Exception> HandleErrorAsync(HttpResponseMessage response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.Unauthorized => new UnauthorizedException(string.Empty),
                HttpStatusCode.Forbidden => new ForbiddenException(string.Empty),
                HttpStatusCode.NotFound => new EntityNotFoundException(string.Empty),
                HttpStatusCode.BadRequest => await CreateBadRequestExceptionAsync(response.Content),
                _ => new UnexpectedHttpErrorException(await response.Content.ReadAsStringAsync()),
            };
        }

        private async Task<BadRequestException> CreateBadRequestExceptionAsync(HttpContent content)
        {
            ErrorResult errorResult;
            try
            {
                errorResult = await JsonContentSerializer.DeserializeAsync<ErrorResult>(content);
            }
            catch (Exception)
            {
                errorResult = new ErrorResult
                {
                    Message = await content.ReadAsStringAsync()
                };
            }

            return new BadRequestException(errorResult.Message, errorResult);
        }

        public async Task<HttpContent> CustomGetAsync(string partialUrl)
        {
            HttpRequestMessage request = GetHttpRequestMessage(HttpMethod.Get, partialUrl);
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            await ValidateSuccessStatusCodeAsync(response);
            return response.Content;
        }

        public async Task PostAsync(string partialUrl)
        {
            HttpRequestMessage request = GetHttpRequestMessage(HttpMethod.Post, partialUrl);
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            await ValidateSuccessStatusCodeAsync(response);
        }

        public async Task PostAsync<TBody>(string partialUrl, TBody body)
        {
            StringContent content = JsonContentSerializer.SerializeToStringContent(body);
            HttpRequestMessage request = GetHttpRequestMessage(HttpMethod.Post, partialUrl);
            request.Content = content;
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            await ValidateSuccessStatusCodeAsync(response);
        }

        public async Task<TResult> PostAsync<TBody, TResult>(string partialUrl, TBody body)
        {
            StringContent content = JsonContentSerializer.SerializeToStringContent(body);
            HttpRequestMessage request = GetHttpRequestMessage(HttpMethod.Post, partialUrl);
            request.Content = content;
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return await HandleResponseAsync<TResult>(response);
        }

        public async Task<TResult> CustomPostAsync<TResult>(HttpRequestMessage httpRequestMessage)
        {
            HttpResponseMessage response = await _httpClient.SendAsync(httpRequestMessage);
            return await HandleResponseAsync<TResult>(response);
        }

        public async Task<TResult> PatchAsync<TBody, TResult>(string partialUrl, TBody body)
        {
            StringContent content = JsonContentSerializer.SerializeToStringContent(body);
            HttpRequestMessage request = GetHttpRequestMessage(HttpMethod.Patch, partialUrl);
            request.Content = content;
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return await HandleResponseAsync<TResult>(response);
        }

        public async Task DeleteAsync(string partialUrl)
        {
            HttpRequestMessage request = GetHttpRequestMessage(HttpMethod.Delete, partialUrl);
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            await ValidateSuccessStatusCodeAsync(response);
        }
    }
}