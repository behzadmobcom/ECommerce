using Entities.Helper;
using Entities.HolooEntity;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IHolooUnitService : IEntityService<HolooUnit>
    {
        Task<ServiceResult<List<HolooUnit>>> Load();
    }
}
