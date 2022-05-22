using Entities.Helper;
using Entities.HolooEntity;

namespace ECommerce.Services.IServices;

public interface IHolooMGroupService : IEntityService<HolooMGroup>
{
    Task<ApiResult<List<HolooMGroup>>> Load();
}