using API.DataContext;
using API.Interface;
using Entities;
using Entities.HolooEntity;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class HolooArticleRepository : HolooRepository<HolooArticle>, IHolooArticleRepository
{
    private readonly HolooDbContext _context;

    public HolooArticleRepository(HolooDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<HolooArticle>> GetAllArticleMCodeSCode(string code,
        CancellationToken cancellationToken, short sendToSite = 0)
    {
        return await _context.ARTICLE.Where(x => x.A_Code.StartsWith(code)
                                                 && x.SendToSite == sendToSite && !x.Model.Equals("#*Not_Article*#"))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<HolooArticle>> GetAllMCode(string mCode, CancellationToken cancellationToken,
        short sendToSite = 0)
    {
        return await _context.ARTICLE
            .Where(x => x.A_Code.StartsWith(mCode) && x.SendToSite == sendToSite && !x.Model.Equals("#*Not_Article*#"))
            .ToListAsync(cancellationToken);
    }

    public async Task SyncHolooWebId(string aCodeC, int productId, CancellationToken cancellationToken)
    {
        var articles =  _context.ARTICLE.Where(x => x.A_Code_C == aCodeC);
        foreach(var article in articles)
        {
            article.WebId = productId;
            _context.ARTICLE.Update(article);
        }
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<HolooArticle>> GetHolooArticles(List<string> aCodeCs, CancellationToken cancellationToken)
    {
        var temp = _context.ARTICLE.Where(
            x => aCodeCs.Any(
                aCodeC => aCodeC == x.A_Code_C));
        return await temp.Where(x => Convert.ToInt32(x.A_Code) < 3400000).ToListAsync(cancellationToken);
    }

    public async Task<(decimal price, double? exist)> GetHolooPrice(string aCodeC, Price.HolooSellNumber sellPrice)
    {
        var article = await _context.ARTICLE.Where(x => x.A_Code_C.Equals(aCodeC) && Convert.ToInt32(x.A_Code) < 3400000).ToListAsync();
        decimal price = 0;
        switch (sellPrice)
        {
            case Price.HolooSellNumber.Sel_Price:
                price = Convert.ToUInt64(article.FirstOrDefault().Sel_Price);
                break;
            case Price.HolooSellNumber.Sel_Price2:
                price = Convert.ToUInt64(article.FirstOrDefault().Sel_Price2);
                break;
            case Price.HolooSellNumber.Sel_Price3:
                price = Convert.ToUInt64(article.FirstOrDefault().Sel_Price3);
                break;
            case Price.HolooSellNumber.Sel_Price4:
                price = Convert.ToUInt64(article.FirstOrDefault().Sel_Price4);
                break;
            case Price.HolooSellNumber.Sel_Price5:
                price = Convert.ToUInt64(article.FirstOrDefault().Sel_Price5);
                break;
            case Price.HolooSellNumber.Sel_Price6:
                price = Convert.ToUInt64(article.FirstOrDefault().Sel_Price6);
                break;
            case Price.HolooSellNumber.Sel_Price7:
                price = Convert.ToUInt64(article.FirstOrDefault().Sel_Price7);
                break;
            case Price.HolooSellNumber.Sel_Price8:
                price = Convert.ToUInt64(article.FirstOrDefault().Sel_Price8);
                break;
            case Price.HolooSellNumber.Sel_Price9:
                price = Convert.ToUInt64(article.FirstOrDefault().Sel_Price9);
                break;
            case Price.HolooSellNumber.Sel_Price10:
                price = Convert.ToUInt64(article.FirstOrDefault().Sel_Price10);
                break;
        }

        return (price, article.Sum(x => x.Exist));
    }
}