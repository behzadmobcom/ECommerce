using Entities;
using Entities.HolooEntity;

namespace API.Interface;

public interface IHolooArticleRepository : IHolooRepository<HolooArticle>
{
    Task<IEnumerable<HolooArticle>> GetAllArticleMCodeSCode(string code, CancellationToken cancellationToken,
        short sendToSite = 0);

    Task<IEnumerable<HolooArticle>>
        GetAllMCode(string mCode, CancellationToken cancellationToken, short sendToSite = 0);

    Task<int> SyncHolooWebId(IEnumerable<HolooArticle> products, CancellationToken cancellationToken);
    Task<(int price, double? exist)> GetHolooPrice(string aCode, Price.HolooSellNumber sellPrice);
}