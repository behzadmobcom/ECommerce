using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;

namespace ECommerce.Services.Services;

public class PriceService : EntityService<Price>, IPriceService
{
    private const string Url = "api/Prices";
    private readonly IHttpService _http;

    public PriceService(IHttpService http) : base(http)
    {
        _http = http;
    }

    public async Task<ServiceResult<List<Price>>> Load(string search = "", int pageNumber = 0, int pageSize = 10)
    {
        var result = await ReadList(Url, $"Get?PageNumber={pageNumber}&PageSize={pageSize}&Search={search}");
        return Return(result);
    }


    public async Task<ServiceResult> Add(Price price)
    {
        var result = await Create(Url, price);
        return Return(result);
    }

    public async Task<ServiceResult> Edit(Price brand)
    {
        var result = await Update(Url, brand);
        return Return(result);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        var result = await Delete(Url, id);
        return Return(result);
    }

    public async Task<ServiceResult<List<Price>>> PriceOfProduct(int productId)
    {
        var result = await ReadList(Url, $"Product/{productId}");
        return Return(result);
    }

    public async Task<ServiceResult<List<PriceViewModel>>> GetAllByViewModel()
    {
        var result = await _http.GetAsync<List<PriceViewModel>>(Url, $"GetAllByViewModel");
        return Return(result);
    }
}