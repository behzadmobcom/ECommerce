using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using Entities.HolooEntity;

namespace API.Interface
{
    public interface IHolooArticleRepository : IHolooRepository<HolooArticle>
    {
        Task<IEnumerable<HolooArticle>> GetAllArticleMCodeSCode(string code, CancellationToken cancellationToken, Int16 sendToSite = 0);

        Task<IEnumerable<HolooArticle>> GetAllMCode(string mCode, CancellationToken cancellationToken, Int16 sendToSite = 0);

        Task<int> SyncHolooWebId(IEnumerable<HolooArticle> products, CancellationToken cancellationToken);
        Task<(int price, double? exist)> GetHolooPrice(string aCode, Price.HolooSellNumber sellPrice);
    }
}
