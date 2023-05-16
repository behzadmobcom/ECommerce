using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ControllersTests.BaseContext.Extension
{
    public abstract class RequesterService : IRequesterService
    {
        private readonly HttpClient _httpClient;

        public RequesterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TResult> GetAsync<TResult>(string partialUrl)
        {
            HttpRequestMessage request = await GetHttpRequestMessageAsync(HttpMethod.Get, partialUrl);
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return await HandleResponseAsync<TResult>(response);
        }

        public abstract Task<HttpRequestMessage> GetHttpRequestMessageAsync(HttpMethod httpMethod, string partialUrl);

        private async Task<TResult> HandleResponseAsync<TResult>(HttpResponseMessage response)
        {
            await ValidateSuccessStatusCodeAsync(response);
            return await BsonContentSerializer.DeserializeAsync<TResult>(response.Content);
        }

        private async Task ValidateSuccessStatusCodeAsync(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw await HandleError(response);
            }
        }

        private async Task<Exception> HandleError(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedException(string.Empty);
                case HttpStatusCode.Forbidden:
                    return new ForbiddenException(string.Empty);
                case HttpStatusCode.NotFound:
                    return new EntityNotFoundException(string.Empty);
                case HttpStatusCode.BadRequest:
                    return await CreateBadRequestException(response.Content);
                default:
                    return new UnexpectedHttpErrorException(await response.Content.ReadAsStringAsync());
            }
        }

        private async Task<BadRequestException> CreateBadRequestException(HttpContent content)
        {
            ErrorResult errorResult;
            try
            {
                errorResult = await BsonContentSerializer.DeserializeAsync<ErrorResult>(content);
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

        public async Task PostAsync(string partialUrl)
        {
            HttpRequestMessage request = await GetHttpRequestMessageAsync(HttpMethod.Post, partialUrl);
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            await ValidateSuccessStatusCodeAsync(response);
        }

        public async Task PostAsync<TBody>(string partialUrl, TBody body)
        {
            ByteArrayContent content = BsonContentSerializer.SerializeToStringContent(body);
            HttpRequestMessage request = await GetHttpRequestMessageAsync(HttpMethod.Post, partialUrl);
            request.Content = content;
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            await ValidateSuccessStatusCodeAsync(response);
        }

        public async Task<TResult> PostAsync<TBody, TResult>(string partialUrl, TBody body)
        {
            ByteArrayContent content = BsonContentSerializer.SerializeToStringContent(body);
            HttpRequestMessage request = await GetHttpRequestMessageAsync(HttpMethod.Post, partialUrl);
            request.Content = content;
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return await HandleResponseAsync<TResult>(response);
        }

        public async Task<TResult> PutAsync<TBody, TResult>(string partialUrl, TBody body)
        {
            ByteArrayContent content = BsonContentSerializer.SerializeToStringContent(body);
            HttpRequestMessage request = await GetHttpRequestMessageAsync(HttpMethod.Put, partialUrl);
            request.Content = content;
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return await HandleResponseAsync<TResult>(response);
        }

        public async Task PutAsync<TBody>(string partialUrl, TBody body)
        {
            ByteArrayContent content = JsonContentSerializer.SerializeToStringContent(body);
            HttpRequestMessage request = await GetHttpRequestMessageAsync(HttpMethod.Put, partialUrl);
            request.Content = content;
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            await ValidateSuccessStatusCodeAsync(response);
        }
        public async Task<TResult> PatchAsync<TBody, TResult>(string partialUrl, TBody body)
        {
            ByteArrayContent content = BsonContentSerializer.SerializeToStringContent(body);
            HttpRequestMessage request = await GetHttpRequestMessageAsync(HttpMethod.Patch, partialUrl);
            request.Content = content;
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return await HandleResponseAsync<TResult>(response);
        }

        public async Task DeleteAsync(string partialUrl)
        {
            HttpRequestMessage request = await GetHttpRequestMessageAsync(HttpMethod.Delete, partialUrl);
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            await ValidateSuccessStatusCodeAsync(response);
        }

        public abstract IRequesterService WithAuthorizationHeaders(AuthorizationHeaders authorizationHeaders);

        public abstract void SetToken(string token);

        public async Task<TResult> CustomPostAsync<TResult>(HttpRequestMessage httpRequestMessage)
        {
            HttpResponseMessage response = await _httpClient.SendAsync(httpRequestMessage);
            return await HandleResponseAsync<TResult>(response);
        }

        public async Task<HttpContent> CustomGetAsync(string partialUrl)
        {
            HttpRequestMessage request = await GetHttpRequestMessageAsync(HttpMethod.Get, partialUrl);
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            await ValidateSuccessStatusCodeAsync(response);
            return response.Content;
        }

        public async Task<TResult> PostAsync<TResult>(string partialUrl, MultipartFormDataContent formDataContent)
        {
            HttpRequestMessage request = await GetHttpRequestMessageAsync(HttpMethod.Post, partialUrl);
            request.Content = formDataContent;
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return await HandleResponseAsync<TResult>(response);
        }
    }
}
