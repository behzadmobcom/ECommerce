using Entities.Helper;
using Entities.HolooEntity;

namespace Services.IServices;

public interface IHolooUnitService : IEntityService<HolooUnit>
{
    Task<ServiceResult<List<HolooUnit>>> Load();
}