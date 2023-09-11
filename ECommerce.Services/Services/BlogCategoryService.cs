using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;

namespace ECommerce.Services.Services;

public class BlogCategoryService : EntityService<BlogCategory>, IBlogCategoryService
{
    private const string Url = "api/BlogCategories";
    private readonly IHttpService _http;

    public BlogCategoryService(IHttpService http) : base(http)
    {
        _http = http;
    }

    public async Task<ServiceResult<List<BlogCategory>>> GetAll(string search = "", int pageNumber = 0, int pageSize = 10)
    {
        var result = await ReadList(Url, $"GetAllWithPagination?PageNumber={pageNumber}&PageSize={pageSize}&Search={search}");
        return Return(result);
    }

    public async Task<ServiceResult<Dictionary<int, string>>> LoadDictionary()
    {
        var result = await ReadList(Url);
        if (result.Code == ResultCode.Success)
            return new ServiceResult<Dictionary<int, string>>
            {
                Code = ServiceCode.Success,
                ReturnData = result.ReturnData.ToDictionary(item => item.Id, item => item.Name),
                Message = result.Messages?.FirstOrDefault()
            };
        return new ServiceResult<Dictionary<int, string>>
        {
            Code = ServiceCode.Error,
            Message = result.GetBody()
        };
    }
    public async Task<ServiceResult> Add(BlogCategory blogCategory)
    {
        var result = await Create(Url, blogCategory);
        return Return(result);
    }

    public async Task<ServiceResult> Edit(BlogCategory blogCategory)
    {
        var result = await Update(Url, blogCategory);
        return Return(result);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        var result = await _http.DeleteAsync(Url, id);
        if (result.Code == ResultCode.Success)
        {
            return new ServiceResult
            {
                Code = ServiceCode.Success,
                Message = "با موفقیت حذف شد"
            };
        }
        return new ServiceResult { Code = ServiceCode.Error, Message = "به علت وابستگی با عناصر دیگر امکان حذف وجود ندارد" };
    }

    public async Task<ServiceResult<BlogCategory>> GetById(int id)
    {
        var result = await _http.GetAsync<BlogCategory>(Url, $"GetById?id={id}");
        return Return(result);
    }

    public async Task<ServiceResult<List<CategoryParentViewModel>>> GetParents(int blogId = 0)
    {
        var result = await _http.GetAsync<List<CategoryParentViewModel>>(Url, $"GetParents?blogId={blogId}");
        if (result.Code == ResultCode.Success)
            return new ServiceResult<List<CategoryParentViewModel>>
            {
                Code = ServiceCode.Success,
                ReturnData = result.ReturnData
            };
        return new ServiceResult<List<CategoryParentViewModel>>
        {
            Code = ServiceCode.Error,
            Message = result.GetBody()
        };
    }
}