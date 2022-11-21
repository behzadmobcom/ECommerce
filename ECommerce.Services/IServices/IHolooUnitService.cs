using Ecommerce.Entities.Helper;
using Ecommerce.Entities.HolooEntity;

namespace ECommerce.Services.IServices;

public interface IHolooUnitService : IEntityService<HolooUnit>
{
    Task<ServiceResult<List<HolooUnit>>> Load();
}