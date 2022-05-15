using Entities.Helper;
using Services.IServices;

namespace Services.Services;

public class EntityService<T> : IEntityService<T>
{
    private readonly IHttpService _http;

    public EntityService(IHttpService http)
    {
        _http = http;
    }

    public async Task<ApiResult<List<T>>> ReadList(string url)
    {
        return await _http.GetAsync<List<T>>(url);
    }

    public async Task<ApiResult<List<T>>> ReadList(string url, string api)
    {
        return await _http.GetAsync<List<T>>(url, api);
    }

    public async Task<ApiResult<T>> Read(string url)
    {
        return await _http.GetAsync<T>(url);
    }

    public async Task<ApiResult<T>> Read(string url, string api)
    {
        return await _http.GetAsync<T>(url, api);
    }

    public async Task<ApiResult<object>> Create(string url, T entity)
    {
        var response = await _http.PostAsync(url, entity);
        if (response == null)
            return new ApiResult<object>
            {
                Code = ResultCode.ServerDontResponse,
                Messages = new List<string> {"سرور سایت در دسترس نیست. لطفا با پشتیبان سایت تماس بگیرید"}
            };
        response.Messages = response.Code > 0
            ? new List<string> {response.GetBody()}
            : new List<string> {"با موفقیت ذخیره شد"};
        return response;
    }

    public async Task<ApiResult<TResponse>> Create<TResponse>(string url, T entity)
    {
        var response = await _http.PostAsync<T, TResponse>(url, entity);
        response.Messages = response.Code > 0
            ? new List<string> {response.GetBody()}
            : new List<string> {"با موفقیت ذخیره شد"};
        return response;
    }

    public async Task<ApiResult> UpdateWithReturnId(string url, T entity)
    {
        var response = await _http.PutAsync(url, entity);
        var messages = response.Code > 0
            ? new List<string> {response.GetBody()}
            : new List<string> {"با موفقیت ویرایش شد"};
        return new ApiResult
        {
            Code = ResultCode.Success,
            Messages = messages
        };
    }

    public async Task<ApiResult> Update(string url, T entity)
    {
        var response = await _http.PutAsync(url, entity);
        response.Messages = response.Code > 0
            ? new List<string> {response.GetBody()}
            : new List<string> {"با موفقیت ویرایش شد"};
        return response;
    }
    public async Task<ApiResult> Update(string url, T entity, string apiName)
    {
        var response = await _http.PutAsync(url, entity, apiName);
        response.Messages = response.Code > 0
            ? new List<string> { response.GetBody() }
            : new List<string> { "با موفقیت ویرایش شد" };
        return response;
    }
    public async Task<ApiResult> Delete(string url, int entityId)
    {
        var response = await _http.DeleteAsync(url, entityId);
        response.Messages = response.Code > 0
            ? new List<string> {response.GetBody()}
            : new List<string> {"با موفقیت حذف شد"};
        return response;
    }

    public ServiceResult<TResult> Return<TResult>(ApiResult<TResult> result)
    {
        if (result is {Code: ResultCode.Success})
            return new ServiceResult<TResult>
            {
                PaginationDetails = result.PaginationDetails,
                Code = ServiceCode.Success,
                ReturnData = result.ReturnData,
                Message = result.Messages?.FirstOrDefault()
            };
        var typeOfTResult = Activator.CreateInstance(typeof(TResult));
        return new ServiceResult<TResult>
        {
            Code = ServiceCode.Error,
            Message = result?.GetBody(),
            ReturnData = (TResult) typeOfTResult
        };
    }

    public ServiceResult Return(ApiResult result)
    {
        if (result.Code == ResultCode.Success)
            return new ServiceResult
            {
                PaginationDetails = result.PaginationDetails,
                Code = ServiceCode.Success,
                Message = result.Messages?.FirstOrDefault()
            };
        return new ServiceResult
        {
            Code = ServiceCode.Error,
            Message = result.GetBody()
        };
    }

    public ServiceResult Return(ApiResult<object> result)
    {
        if (result.Code == ResultCode.Success)
            return new ServiceResult
            {
                PaginationDetails = result.PaginationDetails,
                Code = ServiceCode.Success,
                Message = result.Messages?.FirstOrDefault()
            };
        return new ServiceResult
        {
            Code = ServiceCode.Error,
            Message = result.GetBody()
        };
    }
}