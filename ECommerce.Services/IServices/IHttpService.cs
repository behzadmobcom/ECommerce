﻿using Ecommerce.Entities.Helper;

namespace ECommerce.Services.IServices;

public interface IHttpService
{
    Task<ApiResult<object>> PostAsync<T>(string url, T data, string apiName = "Post");
    Task<ApiResult<TResponse>> PostAsync<T, TResponse>(string url, T data, string apiName = "Post");
    Task<ApiResult> PutAsync<T>(string url, T data, string apiName = "Put");
    Task<ApiResult> DeleteAsync(string url, int id, string apiName = "Delete");
    Task<ApiResult<TResponse>> GetAsync<TResponse>(string url, string apiName = "Get");
    Task<TResponse> PostAsyncWithApiKey<TResponse>(string apiName, string apiKey, string data, string url);
}