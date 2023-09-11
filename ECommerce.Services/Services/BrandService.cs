using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;

namespace ECommerce.Services.Services;

public class BrandService : EntityService<Brand>, IBrandService
{
    private const string Url = "api/Brands";
    private readonly IHttpService _http;

    public BrandService(IHttpService http) : base(http)
    {
        _http = http;
    }

    public async Task<ServiceResult<List<Brand>>> Load()
    {
        var result = await ReadList(Url, "GetAll");
        return Return(result);
    }

    public async Task<ServiceResult<List<Brand>>> GetAll(string search = "", int pageNumber = 0, int pageSize = 10)
    {
        var result = await ReadList(Url,
            $"GetAllWithPagination?PageNumber={pageNumber}&Search={search}&PageSize={pageSize}");
        return Return(result);
    }

    public async Task<ServiceResult<Dictionary<int, string>>> LoadDictionary()
    {
        var result = await ReadList(Url, "GetAll");
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


    public async Task<ServiceResult> Add(Brand brand)
    {
        var result = await Create(Url, brand);
        return Return(result);
    }

    public async Task<ServiceResult> Edit(Brand brand)
    {
        var result = await Update(Url, brand);
        return Return(result);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        //var result = await Delete(Url, id);
        //return Return(result);
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

    public async Task<ServiceResult<Brand>> GetById(int id)
    {
        var result = await _http.GetAsync<Brand>(Url, $"GetById?id={id}");
        return Return(result);
    }
}