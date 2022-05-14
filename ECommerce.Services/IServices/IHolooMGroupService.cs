using Entities.Helper;
using Entities.HolooEntity;

namespace Services.IServices;

public interface IHolooMGroupService : IEntityService<HolooMGroup>
{
    Task<ApiResult<List<HolooMGroup>>> Load();
}