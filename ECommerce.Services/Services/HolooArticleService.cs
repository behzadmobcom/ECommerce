

using Services.IServices;
using Entities;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Helper;
using Entities.HolooEntity;

namespace Services.Services
{
    public class HolooArticleService : EntityService<HolooArticle>, IHolooArticleService
    {
        public HolooArticleService(IHttpService http) : base(http)
        {
        }

        private const string Url = "api/Products";
        public async Task<ServiceResult<List<HolooArticle>>> Load(string code)
        {
            var result = await ReadList(Url,$"GetAllArticleMCodeSCode?code={code}");
            if (result.Code == ResultCode.Success)
                return new ServiceResult<List<HolooArticle>>
                {
                    Code = ServiceCode.Success,
                    ReturnData = result.ReturnData,
                    Message = result.Messages?.FirstOrDefault()
                };
            return new ServiceResult<List<HolooArticle>>
            {
                Code = ServiceCode.Error,
                Message = result.GetBody()
            };
        }

    }
}
