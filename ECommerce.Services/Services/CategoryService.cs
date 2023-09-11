using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;

namespace ECommerce.Services.Services;

public class CategoryService : EntityService<Category>, ICategoryService
{
    private const string Url = "api/Categories";
    private readonly IEntityService<CategoryViewModel> _categoryViewModelEntityService;
    private readonly IHttpService _http;


    public CategoryService(IHttpService http, IEntityService<CategoryViewModel> categoryViewModelEntityService) :
        base(http)
    {
        _http = http;
        _categoryViewModelEntityService = categoryViewModelEntityService;
    }

    public async Task<ServiceResult<CategoryViewModel>> GetByUrl(string url)
    {
        var result = await _http.GetAsync<CategoryViewModel>(Url, $"GetByUrl?url={url}");
        if (result.Code == ResultCode.Success)
            return new ServiceResult<CategoryViewModel>
            {
                Code = ServiceCode.Success,
                ReturnData = result.ReturnData
            };
        return new ServiceResult<CategoryViewModel>
        {
            Code = ServiceCode.Error,
            Message = result.GetBody()
        };
    }

    public async Task<ServiceResult<List<CategoryParentViewModel>>> GetParents(int productId = 0)
    {
        var result = await _http.GetAsync<List<CategoryParentViewModel>>(Url, $"GetParents?productId={productId}");
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

    public async Task<ServiceResult<List<Category>>> Load(string search = "", int pageNumber = 0, int pageSize = 10)
    {
        var result = await ReadList(Url, $"Get?PageNumber={pageNumber}&PageSize={pageSize}&Search={search}");
        if (result.Code == ResultCode.Success)
            return new ServiceResult<List<Category>>
            {
                Code = ServiceCode.Success,
                ReturnData = result.ReturnData,
                PaginationDetails = result.PaginationDetails
            };
        return new ServiceResult<List<Category>>
        {
            Code = ServiceCode.Error,
            Message = result.GetBody()
        };
    }

    public async Task<ServiceResult<List<TreeViewModel>>> LoadTree()
    {
        var result = await ReadList(Url);
        if (result.Code == ResultCode.Success)
            return new ServiceResult<List<TreeViewModel>>
            {
                Code = ServiceCode.Success,
                ReturnData = result.ReturnData
                    .Select(x => new TreeViewModel {Id = x.Id, Name = x.Name, ParentId = x.ParentId}).ToList()
            };
        return new ServiceResult<List<TreeViewModel>>
        {
            Code = ServiceCode.Error,
            Message = result.GetBody()
        };
    }

    public async Task<ServiceResult> Add(Category category)
    {
        var result = await Create(Url, category);
        return Return(result);
    }

    public async Task<ServiceResult> Edit(Category category)
    {
        var result = await Update(Url, category);
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

    public async Task<ServiceResult<List<CategoryViewModel>>> GetCategoriesByProductId(int productId)
    {
        var result =
            await _categoryViewModelEntityService.ReadList(Url, $"GetCategoriesByProduct?productId={productId}");
        return Return(result);
    }

    public async Task<ServiceResult<Category>> GetById(int id)
    {
        var result = await _http.GetAsync<Category>(Url, $"GetById?id={id}");
        return Return(result);
    }

    public async Task<ServiceResult<List<Category>>> Search(string searchKeyword)
    {
        var result = await _http.GetAsync<List<Category>>(Url, $"Search?searchKeyword={searchKeyword}");
        return Return(result);
    }
}