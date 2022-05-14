﻿namespace Entities.Helper;

public class ResponseData<T>
{
    public ResponseData(T response, bool success, HttpResponseMessage httpResponseMessage)
    {
        Success = success;
        Response = response;
        HttpResponseMessage = httpResponseMessage;
    }

    public bool Success { get; set; }
    public T Response { get; set; }
    public HttpResponseMessage HttpResponseMessage { get; set; }

    public async Task<string> GetBody()
    {
        return await HttpResponseMessage.Content.ReadAsStringAsync();
    }
}