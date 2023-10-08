using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Authentication;

namespace ECommerce.Services.Services;

public class HttpService : IHttpService
{
    private readonly ICookieService _cookieService;

    private readonly HttpClient _http;

    public HttpService(HttpClient http, ICookieService cookieService)
    {
        _http = http;
        _cookieService = cookieService;
    }

    private JsonSerializerOptions DefaultJsonSerializerOptions => new() { PropertyNameCaseInsensitive = true };

    public async Task<ApiResult<object>> PostAsyncWithoutToken<T>(string url, T data, string apiName = "Post")
    {
        var CreatorUserId = data.GetType().GetProperty("CreatorUserId");
        if (CreatorUserId != null)
        {
            var resultCurrentUser = _cookieService.GetCurrentUser();
            if (CreatorUserId.GetValue(data) == null)
                CreatorUserId.SetValue(data, resultCurrentUser.Id);
        }

        var dataSerialize = JsonSerializer.Serialize(data);
        var content = new StringContent(dataSerialize, Encoding.UTF8, "application/json");
        var response = await _http.PostAsync($"{url}/{apiName}", content);

        var responseDeserialized = await Deserialize<ApiResult<object>>(response, DefaultJsonSerializerOptions);
        return responseDeserialized;
    }

    public async Task<ApiResult<object>> PostAsync<T>(string url, T data, string apiName = "Post")
    {
        var loginResult = _cookieService.GetToken();
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult);

        var CreatorUserId = data.GetType().GetProperty("CreatorUserId");
        if (CreatorUserId != null)
        {
            var resultCurrentUser = _cookieService.GetCurrentUser();
            if (CreatorUserId.GetValue(data) == null)
                CreatorUserId.SetValue(data, resultCurrentUser.Id);
        }

        var dataSerialize = JsonSerializer.Serialize(data);
        var content = new StringContent(dataSerialize, Encoding.UTF8, "application/json");
        var response = await _http.PostAsync($"{url}/{apiName}", content);

        var responseDeserialized = await Deserialize<ApiResult<object>>(response, DefaultJsonSerializerOptions);
        return responseDeserialized;
    }

    public async Task<ApiResult<TResponse>> PostAsync<T, TResponse>(string url, T data, string apiName = "Post")
    {
        var loginResult = _cookieService.GetToken();
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult);

        var CreatorUserId = data.GetType().GetProperty("CreatorUserId");
        if (CreatorUserId != null)
        {
            var resultCurrentUser = _cookieService.GetCurrentUser();
            if (CreatorUserId.GetValue(data) == null)
                CreatorUserId.SetValue(data, resultCurrentUser.Id);
        }

        var dataSerialize = JsonSerializer.Serialize(data);
        var content = new StringContent(dataSerialize, Encoding.UTF8, "application/json");
        var response = await _http.PostAsync($"{url}/{apiName}", content);

        var responseData = new ResponseData<ApiResult<TResponse>>(default, false, response);
        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<ApiResult<TResponse>>(response, DefaultJsonSerializerOptions);
            return responseDeserialized;
        }

        if (responseData.Response == null)
            return new ApiResult<TResponse> { Code = ResultCode.BadRequest };
        return responseData.Response;
    }

    public async Task<ApiResult> PutAsync<T>(string url, T data, string apiName = "Put")
    {
        var loginResult = _cookieService.GetToken();
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult);

        var EditorUserId = data.GetType().GetProperty("EditorUserId");
        if (EditorUserId != null)
        {
            var resultCurrentUser = _cookieService.GetCurrentUser();
            EditorUserId.SetValue(data, resultCurrentUser.Id);
        }

        var dataSerialize = JsonSerializer.Serialize(data);
        var content = new StringContent(dataSerialize, Encoding.UTF8, "application/json");
        var response = await _http.PutAsync($"{url}/{apiName}", content);

        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<ApiResult>(response, DefaultJsonSerializerOptions);
            return responseDeserialized;
        }

        return new ApiResult { Code = ResultCode.BadRequest };
    }

    public async Task<ApiResult> DeleteAsync(string url, int id, string apiName = "Delete")
    {
        var loginResult = _cookieService.GetToken();
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult);

        var response = await _http.DeleteAsync($"{url}/{apiName}?id={id}");

        var responseData = new ResponseData<ApiResult>(default, false, response);
        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<ApiResult>(response, DefaultJsonSerializerOptions);
            return responseDeserialized;
        }

        return new ApiResult { Code = ResultCode.BadRequest };
    }

    public async Task<ApiResult<TResponse>> GetAsync<TResponse>(string url, string apiName = "Get")
    {
        var loginResult = _cookieService.GetToken();
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult);

        var response = await _http.GetAsync($"{url}/{apiName}");

        //HttpHeaders headers = response.Headers;
        //IEnumerable<string> values;
        //if (headers.TryGetValues("X-Pagination", out values))
        //{
        //    string session = values.First();
        //}

        var responseData = new ResponseData<ApiResult<TResponse>>(default, false, response);
        if (!response.IsSuccessStatusCode) return responseData.Response;

        var responseDeserialized = await Deserialize<ApiResult<TResponse>>(response, DefaultJsonSerializerOptions);
        return responseDeserialized;
    }

    public async Task<ApiResult<TResponse>> GetAsync<T, TResponse>(string url, T data, string apiName = "Get")
    {
        var loginResult = _cookieService.GetToken();
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult);

        var dataSerialize = JsonSerializer.Serialize(data);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(url),
            Content = new StringContent(dataSerialize, Encoding.UTF8, "application/json")
        };
        var response = await _http.SendAsync(request);

        //HttpHeaders headers = response.Headers;
        //IEnumerable<string> values;
        //if (headers.TryGetValues("X-Pagination", out values))
        //{
        //    string session = values.First();
        //}

        var responseData = new ResponseData<ApiResult<TResponse>>(default, false, response);
        if (!response.IsSuccessStatusCode) return responseData.Response;

        var responseDeserialized = await Deserialize<ApiResult<TResponse>>(response, DefaultJsonSerializerOptions);
        return responseDeserialized;
    }

    private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
    {
        var responseString = await httpResponse.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseString, options);
    }

    public async Task<TResponse> PostAsyncWithApiKeyByRequestModel<TRequest, TResponse>(string apiName, string apiKey, TRequest data, string url)
    {
        _http.DefaultRequestHeaders.Add(apiName, apiKey);
        var dataSerialize = JsonSerializer.Serialize(data);
        HttpContent content = new StringContent(dataSerialize, System.Text.Encoding.UTF8, "application/json");
        var response = await _http.PostAsync(url, content);
        var responseData = new ResponseData<ApiResult<TResponse>>(default, false, response);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<TResponse>(responseString, DefaultJsonSerializerOptions);
        return result;
    }

}