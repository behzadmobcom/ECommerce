using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using API.DataContext;
using API.Interface;
using Entities;
using Entities.HolooEntity;

using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class HolooArticleRepository : HolooRepository<HolooArticle>, IHolooArticleRepository
    {
        private readonly HolooDbContext _context;
        public HolooArticleRepository(HolooDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HolooArticle>> GetAllArticleMCodeSCode(string code, CancellationToken cancellationToken, Int16 sendToSite = 0) => await _context.ARTICLE.Where(x => x.A_Code.StartsWith(code)
               && x.SendToSite == sendToSite && !x.Model.Equals("#*Not_Article*#")).ToListAsync(cancellationToken);

        public async Task<IEnumerable<HolooArticle>> GetAllMCode(string mCode, CancellationToken cancellationToken, Int16 sendToSite = 0) => await _context.ARTICLE.Where(x => x.A_Code.StartsWith(mCode) && x.SendToSite == sendToSite && !x.Model.Equals("#*Not_Article*#")).ToListAsync(cancellationToken);

        public async Task<int> SyncHolooWebId(IEnumerable<HolooArticle> holooArticles, CancellationToken cancellationToken)
        {
            //_context.ARTICLE.UpdateRange(holooArticles);
            foreach (var holooArticle in holooArticles)
            {
                _context.Entry(holooArticle).State = EntityState.Modified;
            }
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<(int price, double? exist)> GetHolooPrice(string aCode, Price.HolooSellNumber sellPrice)
        {
            var article = await _context.ARTICLE.FirstOrDefaultAsync(x => x.A_Code.Equals(aCode));
            var price = 0;
            switch (sellPrice)
            {
                case Price.HolooSellNumber.Sel_Price:
                    price = Convert.ToInt32(article.Sel_Price);
                    break;
                case Price.HolooSellNumber.Sel_Price2:
                    price = Convert.ToInt32(article.Sel_Price2);
                    break;
                case Price.HolooSellNumber.Sel_Price3:
                    price = Convert.ToInt32(article.Sel_Price3);
                    break;
                case Price.HolooSellNumber.Sel_Price4:
                    price = Convert.ToInt32(article.Sel_Price4);
                    break;
                case Price.HolooSellNumber.Sel_Price5:
                    price = Convert.ToInt32(article.Sel_Price5);
                    break;
                case Price.HolooSellNumber.Sel_Price6:
                    price = Convert.ToInt32(article.Sel_Price6);
                    break;
                case Price.HolooSellNumber.Sel_Price7:
                    price = Convert.ToInt32(article.Sel_Price7);
                    break;
                case Price.HolooSellNumber.Sel_Price8:
                    price = Convert.ToInt32(article.Sel_Price8);
                    break;
                case Price.HolooSellNumber.Sel_Price9:
                    price = Convert.ToInt32(article.Sel_Price9);
                    break;
                case Price.HolooSellNumber.Sel_Price10:
                    price = Convert.ToInt32(article.Sel_Price10);
                    break;
            }

            return (price, article.Exist);
        }
    }
}
