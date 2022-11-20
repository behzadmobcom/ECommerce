using Ecommerce.Entities.Helper;
using Ecommerce.Entities.HolooEntity;
using ECommerce.Services.IServices;

namespace ECommerce.Services.Services;

public class HolooMGroupService : EntityService<HolooMGroup>, IHolooMGroupService
{
    private const string Url = "api/Products";

    public HolooMGroupService(IHttpService http) : base(http)
    {
    }

    public async Task<ApiResult<List<HolooMGroup>>> Load()
    {
        return await ReadList(Url, "GetMGroup");
    }
}