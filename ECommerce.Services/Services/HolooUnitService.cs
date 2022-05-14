using Entities.Helper;
using Entities.HolooEntity;
using Services.IServices;

namespace Services.Services;

public class HolooUnitService : EntityService<HolooUnit>, IHolooUnitService
{
    private const string Url = "api/Units";

    public HolooUnitService(IHttpService http) : base(http)
    {
    }

    public async Task<ServiceResult<List<HolooUnit>>> Load()
    {
        var result = await ReadList(Url, "GetHolooUnits");
        return Return(result);
    }
}