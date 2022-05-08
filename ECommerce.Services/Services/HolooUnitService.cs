

using Services.IServices;
using Entities.HolooEntity;

using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Helper;

namespace Services.Services
{
    public class HolooUnitService : EntityService<HolooUnit>, IHolooUnitService
    {
        public HolooUnitService(IHttpService http) : base(http)
        {
        }
        private const string Url = "api/Units";
        public async Task<ServiceResult<List<HolooUnit>>> Load()
        {
            var result =await ReadList(Url, "GetHolooUnits");
            return Return<List<HolooUnit>>(result);
        }
    }
}
