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

    public async Task<IEnumerable<HolooArticle>> GetHolooArticle(List<string> aCodes, CancellationToken cancellationToken)
    {
        return await _context.ARTICLE.Where(x => aCodes.Any(aCode => aCode == x.A_Code)).ToListAsync(cancellationToken);
    }

    public async Task<(decimal price, double? exist)> GetHolooPrice(string aCode, Price.HolooSellNumber sellPrice)
    {
        var article = await _context.ARTICLE.FirstOrDefaultAsync(x => x.A_Code.Equals(aCode));
        decimal price = 0;
        switch (sellPrice)
        {
            case Price.HolooSellNumber.Sel_Price:
                price = Convert.ToUInt64(article.Sel_Price);
                break;
            case Price.HolooSellNumber.Sel_Price2:
                price = Convert.ToUInt64(article.Sel_Price2);
                break;
            case Price.HolooSellNumber.Sel_Price3:
                price = Convert.ToUInt64(article.Sel_Price3);
                break;
            case Price.HolooSellNumber.Sel_Price4:
                price = Convert.ToUInt64(article.Sel_Price4);
                break;
            case Price.HolooSellNumber.Sel_Price5:
                price = Convert.ToUInt64(article.Sel_Price5);
                break;
            case Price.HolooSellNumber.Sel_Price6:
                price = Convert.ToUInt64(article.Sel_Price6);
                break;
            case Price.HolooSellNumber.Sel_Price7:
                price = Convert.ToUInt64(article.Sel_Price7);
                break;
            case Price.HolooSellNumber.Sel_Price8:
                price = Convert.ToUInt64(article.Sel_Price8);
                break;
            case Price.HolooSellNumber.Sel_Price9:
                price = Convert.ToUInt64(article.Sel_Price9);
                break;
            case Price.HolooSellNumber.Sel_Price10:
                price = Convert.ToUInt64(article.Sel_Price10);
                break;
        }

        return (price, article.Exist);
    }
}