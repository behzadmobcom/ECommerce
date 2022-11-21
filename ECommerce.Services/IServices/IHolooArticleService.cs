using Ecommerce.Entities.Helper;
using Ecommerce.Entities.HolooEntity;

namespace ECommerce.Services.IServices;

public interface IHolooArticleService
{
    Task<ServiceResult<List<HolooArticle>>> Load(string code);
}