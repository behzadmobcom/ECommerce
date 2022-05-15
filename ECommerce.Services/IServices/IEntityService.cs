using Entities.Helper;

namespace Services.IServices;

public interface IEntityService<T>
{
    Task<ApiResult<List<T>>> ReadList(string url);
    Task<ApiResult<List<T>>> ReadList(string url, string api);
    Task<ApiResult<T>> Read(string url);
    Task<ApiResult<T>> Read(string url, string api);
    Task<ApiResult<object>> Create(string url, T entity);
    Task<ApiResult<TResponse>> Create<TResponse>(string url, T entity);
    Task<ApiResult> Update(string url, T entity);
    Task<ApiResult> Update(string url, T entity, string apiName);
    Task<ApiResult> UpdateWithReturnId(string url, T entity);
    Task<ApiResult> Delete(string url, int id);
    ServiceResult Return(ApiResult result);
    ServiceResult<TResult> Return<TResult>(ApiResult<TResult> result);
    ServiceResult Return(ApiResult<object> result);
}