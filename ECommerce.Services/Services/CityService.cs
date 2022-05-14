using Entities;
using Entities.Helper;
using Services.IServices;

namespace Services.Services;

public class CityService : EntityService<City>, ICityService
{
    private const string Url = "api/Cities";
    private List<City> _cities;

    public CityService(IHttpService http) : base(http)
    {
    }

    public async Task<ServiceResult<List<City>>> Load(int id)
    {
        var result = await ReadList(Url, $"GetById?id={id}");
        return Return(result);
    }

    public async Task<ServiceResult<List<City>>> Filtering(string filter, int id)
    {
        if (_cities == null)
        {
            var cities = await Load(id);
            if (cities.Code > 0) return cities;
            _cities = cities.ReturnData;
        }

        var result = _cities.Where(x => x.Name.Contains(filter)).ToList();
        if (result.Count == 0) return new ServiceResult<List<City>> {Code = ServiceCode.Info, Message = "شهر یافت نشد"};
        return new ServiceResult<List<City>>
        {
            Code = ServiceCode.Success,
            ReturnData = result
        };
    }

    public async Task<ServiceResult> Add(City city)
    {
        var result = await Create(Url, city);
        _cities = null;
        return Return(result);
    }

    public async Task<ServiceResult> Edit(City city)
    {
        var result = await Update(Url, city);
        _cities = null;
        return Return(result);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        var result = await Delete(Url, id);
        _cities = null;
        return Return(result);
    }
}