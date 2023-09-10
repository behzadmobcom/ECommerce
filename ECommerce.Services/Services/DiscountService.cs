using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
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

    public async Task<ServiceResult<DiscountViewModel>> Add(DiscountViewModel discountViewModel)
    {
        var result = await _http.PostAsync<DiscountViewModel, DiscountViewModel>(Url, discountViewModel);
        return Return(result);
    }

    public async Task<ServiceResult> Edit(Discount discount)
    {
        var result = await Update(Url, discount);
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

    public async Task<ServiceResult<Discount>> GetById(int id)
    {
        var result = await _http.GetAsync<Discount>(Url, $"GetById?id={id}");
        return Return(result);
    }

    public async Task<ServiceResult<Discount>> GetByCode(string code)
    {
        var result = await _http.GetAsync<Discount>(Url, $"GetByCode?code={code}");
        return Return(result);
    }

    public async Task<ServiceResult<Discount>> GetLast()
    {
        var result = await _http.GetAsync<Discount>(Url, $"GetLast");
        return Return(result);
    }

    public async Task<bool> Activate(int discountId)
    {
        var id = discountId;
        var result = await _http.GetAsync<bool>(Url, $"ActiveDiscount?id={id}");
        return result.ReturnData;
    }
}