using Entities.Helper;
using Entities.HolooEntity;

namespace ECommerce.Services.IServices;

public interface IHolooSGroupService : IEntityService<HolooSGroup>
{
    Task<ServiceResult<List<HolooSGroup>>> Load(string mGroupCode);
}