using Entities;
using Entities.Helper;
using ECommerce.Services.IServices;

namespace Services.Services;

public class SizeService : EntityService<Size>, ISizeService
{
    private const string Url = "api/Sizes";
    private readonly IHttpService _http;
    private List<Size> _sizes;


    public SizeService(IHttpService http) : base(http)
    {
        _http = http;
    }

    public async Task<ServiceResult<List<Size>>> Load(string search = "", int pageNumber = 0, int pageSize = 10)
    {
        var result = await ReadList(Url, $"Get?PageNumber={pageNumber}&PageSize={pageSize}&Search={search}");
        return Return(result);
    }

    public async Task<ServiceResult<List<Size>>> Filtering(string filter)
    {
        if (_sizes == null)
        {
            var sizes = await Load();
            if (sizes.Code > 0) return sizes;
            _sizes = sizes.ReturnData;
        }

        var result = _sizes.Where(x => x.Name.Contains(filter)).ToList();
        if (result.Count == 0)
            return new ServiceResult<List<Size>> {Code = ServiceCode.Info, Message = "سایزی یافت نشد"};
        return new ServiceResult<List<Size>>
        {
            Code = ServiceCode.Success,
            ReturnData = result
        };
    }

    public async Task<ServiceResult> Add(Size size)
    {
        var result = await Create(Url, size);
        _sizes = null;
        return Return(result);
    }

    public async Task<ServiceResult> Edit(Size size)
    {
        var result = await Update(Url, size);
        _sizes = null;
        return Return(result);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        var result = await Delete(Url, id);
        _sizes = null;
        return Return(result);
    }

    public async Task<ServiceResult<Size>> GetById(int id)
    {
        var result = await _http.GetAsync<Size>(Url, $"GetById?id={id}");
        return Return(result);
    }
}