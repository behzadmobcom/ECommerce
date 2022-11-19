using Entities;
using Entities.HolooEntity;

namespace API.Interface;

public interface IHolooArticleRepository : IHolooRepository<HolooArticle>
{
    Task<IEnumerable<HolooArticle>> GetAllArticleMCodeSCode(string code, CancellationToken cancellationToken,
        short sendToSite = 0);

    Task<IEnumerable<HolooArticle>>
        GetAllMCode(string mCode, CancellationToken cancellationToken, short sendToSite = 0);

    Task SyncHolooWebId(string aCodeC, int productId, CancellationToken cancellationToken);
    Task<IEnumerable<HolooArticle>> GetHolooArticles(List<string> aCodeCs, CancellationToken cancellationToken);
    Task<(decimal price, double? exist)> GetHolooPrice(string aCodeC, Price.HolooSellNumber sellPrice);
}