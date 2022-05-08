using Entities.HolooEntity;

using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using Entities.Helper;

namespace Services.IServices
{
    public interface IHolooSGroupService : IEntityService<HolooSGroup>
    {
       Task<ServiceResult<List<HolooSGroup>>> Load(string mGroupCode);
    }
}
