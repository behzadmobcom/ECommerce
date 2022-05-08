

using Services.IServices;
using Entities.HolooEntity;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Helper;

namespace Services.Services
{
    public class HolooSGroupService : EntityService<HolooSGroup>, IHolooSGroupService
    {
        public HolooSGroupService(IHttpService http) : base(http)
        {
        }
        private const string Url = "api/Products";

        public async Task<ServiceResult<List<HolooSGroup>>> Load(string mGroupCode)
        {
           var result = await ReadList(Url,$"GetSGroupByMGroupCode?mCode={mGroupCode}");
           if (result.Code == ResultCode.Success)
               return new ServiceResult<List<HolooSGroup>>
               {
                   Code = ServiceCode.Success,
                   ReturnData = result.ReturnData,
                   Message = result.Messages?.FirstOrDefault()
               };
           return new ServiceResult<List<HolooSGroup>>
           {
               Code = ServiceCode.Error,
               Message = result.GetBody()
           };
        }
    }
}
