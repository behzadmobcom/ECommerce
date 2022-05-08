using Entities.HolooEntity;

using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Helper;

namespace Services.IServices
{
    public interface IHolooMGroupService : IEntityService<HolooMGroup>
    {
        Task<ApiResult<List<HolooMGroup>>> Load();
    }
}
