using Entities;

using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Helper;
using Entities.HolooEntity;

namespace Services.IServices
{
    public interface IHolooArticleService
    {
        Task<ServiceResult<List<HolooArticle>>> Load(string code);
    }
}
