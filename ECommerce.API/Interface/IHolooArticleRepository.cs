using Ecommerce.Entities;
using Ecommerce.Entities.HolooEntity;
using Ecommerce.Entities.ViewModel;

namespace ECommerce.API.Interface;

public interface IHolooArticleRepository : IHolooRepository<HolooArticle>
{
    Task<IEnumerable<HolooArticle>> GetAllArticleMCodeSCode(string code, CancellationToken cancellationToken,
        short sendToSite = 0);

    Task<IEnumerable<HolooArticle>>
        GetAllMCode(string mCode, CancellationToken cancellationToken, short sendToSite = 0);

    Task SyncHolooWebId(string aCodeC, int productId, CancellationToken cancellationToken);

    Task<IEnumerable<HolooArticle>> GetHolooArticles(List<string> aCodeCs, CancellationToken cancellationToken);

    Task<(decimal price, double? exist)> GetHolooPrice(string aCodeC, Price.HolooSellNumber sellPrice);

    Task<List<T>> AddPriceAndExistFromHolooList<T>(
        IList<T> products, bool isWithoutBill, bool? isCheckExist, CancellationToken cancellationToken) where T : BaseProductPageViewModel;

    Task<List<Price>> AddPrice(List<Price> prices, IEnumerable<HolooArticle> holooArticles, bool isWithoutBill, bool? isCheckExist,
        CancellationToken cancellationToken);
}