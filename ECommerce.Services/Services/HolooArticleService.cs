using Entities.Helper;
using Entities.HolooEntity;
using Services.IServices;

namespace Services.Services;

public class HolooArticleService : EntityService<HolooArticle>, IHolooArticleService
{
    private const string Url = "api/Products";

    public HolooArticleService(IHttpService http) : base(http)
    {
    }

    public async Task<ServiceResult<List<HolooArticle>>> Load(string code)
    {
        var result = await ReadList(Url, $"GetAllArticleMCodeSCode?code={code}");
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