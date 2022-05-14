using Entities.Helper;
using Entities.HolooEntity;

namespace Services.IServices;

public interface IHolooSGroupService : IEntityService<HolooSGroup>
{
    Task<ServiceResult<List<HolooSGroup>>> Load(string mGroupCode);
}