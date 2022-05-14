using Entities;
using Entities.Helper;
using Services.IServices;

namespace Services.Services;

public class SupplierService : EntityService<Supplier>, ISupplierService
{
    private const string Url = "api/Suppliers";
    private readonly IHttpService _http;
    private List<Supplier> _supplier;

    public SupplierService(IHttpService http) : base(http)
    {
        _http = http;
    }

    //
    public async Task<ServiceResult<List<Supplier>>> Load(string search = "", int pageNumber = 0, int pageSize = 10)
    {
        var result = await ReadList(Url, $"Get?PageNumber={pageNumber}&PageSize={pageSize}&Search={search}");
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

    public async Task<ServiceResult<List<Supplier>>> Filtering(string filter)
    {
        if (_supplier == null)
        {
            var supplier = await Load();
            if (supplier.Code > 0) return supplier;
            _supplier = supplier.ReturnData;
        }

        var result = _supplier.Where(x => x.Name.Contains(filter)).ToList();
        if (result.Count == 0)
            return new ServiceResult<List<Supplier>> {Code = ServiceCode.Info, Message = "تامین کننده ای یافت نشد"};
        return new ServiceResult<List<Supplier>>
        {
            Code = ServiceCode.Success,
            ReturnData = result
        };
    }

    public async Task<ServiceResult> Add(Supplier supplier)
    {
        var result = await Create(Url, supplier);
        _supplier = null;
        return Return(result);
    }

    public async Task<ServiceResult> Edit(Supplier supplier)
    {
        var result = await Update(Url, supplier);
        _supplier = null;
        return Return(result);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        var result = await Delete(Url, id);
        _supplier = null;
        return Return(result);
    }

    public async Task<ServiceResult<Supplier>> GetById(int id)
    {
        var result = await _http.GetAsync<Supplier>(Url, $"GetById?id={id}");
        return Return(result);
    }
}