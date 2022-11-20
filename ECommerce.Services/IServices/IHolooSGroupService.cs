using Ecommerce.Entities.Helper;
using Ecommerce.Entities.HolooEntity;

namespace ECommerce.Services.IServices;

public interface IHolooSGroupService : IEntityService<HolooSGroup>
{
    Task<ServiceResult<List<HolooSGroup>>> Load(string mGroupCode);
}