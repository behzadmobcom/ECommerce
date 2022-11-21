using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;

namespace ECommerce.Services.Services;

public class DiscountService : EntityService<Discount>, IDiscountService
{
    private const string Url = "api/Discounts";
    private readonly IHttpService _http;

    public DiscountService(IHttpService http) : base(http)
    {
        _http = http;
    }

    public async Task<ServiceResult<List<Discount>>> Load(string search = "", int pageNumber = 0, int pageSize = 10)
    {
        var result = await ReadList(Url, $"Get?PageNumber={pageNumber}&PageSize={pageSize}&Search={search}");
        return Return(result);
    }


    public async Task<ServiceResult> Add(Discount discount)
    {
        var result = await Create(Url, discount);
        return Return(result);
    }

    public async Task<ServiceResult> Edit(Discount discount)
    {
        var result = await Update(Url, discount);
        return Return(result);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        var result = await Delete(Url, id);
        return Return(result);
    }

    public async Task<ServiceResult<Discount>> GetById(int id)
    {
        var result = await _http.GetAsync<Discount>(Url, $"GetById?id={id}");
        return Return(result);
    }

    public async Task<ServiceResult<Discount>> GetLast()
    {
        var result = await _http.GetAsync<Discount>(Url, $"GetLast");
        return Return(result);
    }
}