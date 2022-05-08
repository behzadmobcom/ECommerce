

using Services.IServices;
using Entities.HolooEntity;

using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Helper;

namespace Services.Services
{
    public class HolooMGroupService : EntityService<HolooMGroup>, IHolooMGroupService
    {
        public HolooMGroupService(IHttpService http) : base(http)
        {
        }

        private const string Url = "api/Products";

        public async Task<ApiResult<List<HolooMGroup>>> Load()
        {
           return await ReadList(Url, "GetMGroup");
        }
    }
}
