using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.HolooEntity;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Entities.ViewModel;
using ECommerce.API.Controllers;

namespace ECommerce.API.Repository;

public class HolooArticleRepository : HolooRepository<HolooArticle>, IHolooArticleRepository
{
    private readonly HolooDbContext _context;
    private readonly ILogger<ProductsController> _logger;
    private readonly IHolooABailRepository _aBailRepository;
    private IConfiguration _configuration;

    public HolooArticleRepository(
        HolooDbContext context,
        ILogger<ProductsController> logger,
        IHolooABailRepository aBailRepository,
        IConfiguration configuration) : base(context)
    {
        _context = context;
        _logger = logger;
        _aBailRepository = aBailRepository;
        _configuration = configuration;
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
        var bolooryFlag = (await _context.Customer.FirstAsync()).C_Name == "گروه تجهيزات صنعتي بلوري";
        return await temp.Where(x => Convert.ToInt32(x.A_Code) < 3400000 || !bolooryFlag).ToListAsync(cancellationToken);
    }

    public async Task<(decimal price, double? exist)> GetHolooPrice(string aCodeC, Price.HolooSellNumber sellPrice)
    {
        var bolooryFlag = (await _context.Customer.FirstAsync()).C_Name == "گروه تجهيزات صنعتي بلوري";
        var article = await _context.ARTICLE.Where(x => x.A_Code_C.Equals(aCodeC) && (Convert.ToInt32(x.A_Code) < 3400000 || !bolooryFlag)).ToListAsync();
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

    public async Task<List<T>> AddPriceAndExistFromHolooList<T>(
        IList<T> products, bool isWithoutBill, bool? isCheckExist, CancellationToken cancellationToken) where T : BaseProductPageViewModel
    {
        isWithoutBill = true;
        var prices = products
            .Where(x => x.Prices.Any(p => p.ArticleCode != null))
            .Select(p => p.Prices)
            .ToList();
        var aCodeCs = new List<string>();
        foreach (var price in prices)
        {
            foreach (var aCode in price)
            {
                aCodeCs.Add(aCode.ArticleCodeCustomer);
            };
        }

        products = products.Where(x => x.Prices.Any(p => p.ArticleCode != null)).ToList();
        var holooArticle = await GetHolooArticles(aCodeCs, cancellationToken);

        List<T> newProducts = new();
        foreach (var product in products)
        {
            List<Price> tempPriceList = await AddPrice(product.Prices, holooArticle, isCheckExist, cancellationToken);

            product.Prices.RemoveAll(x => !tempPriceList.Contains(x));
            if (product.Prices.Count != 0) newProducts.Add(product);
        };

        return newProducts;
    }

    public async Task<List<Price>> AddPrice(List<Price> prices, IEnumerable<HolooArticle> holooArticles, bool? isCheckExist, CancellationToken cancellationToken)
    {

        List<Price> tempPriceList = new();
        foreach (var productPrices in prices)
        {
            if (productPrices.SellNumber != null && productPrices.SellNumber != Price.HolooSellNumber.خالی)
            {
                var article = holooArticles.Where(x => x.A_Code_C == productPrices.ArticleCodeCustomer).ToList();
                decimal articlePrice = 0;
                if (article.Count > 0)
                {
                    try
                    {
                        switch (productPrices.SellNumber)
                        {
                            case Price.HolooSellNumber.Sel_Price:
                                articlePrice = Convert.ToDecimal(article.FirstOrDefault().Sel_Price);
                                break;
                            case Price.HolooSellNumber.Sel_Price2:
                                articlePrice = Convert.ToDecimal(article.FirstOrDefault().Sel_Price2);
                                break;
                            case Price.HolooSellNumber.Sel_Price3:
                                articlePrice = Convert.ToDecimal(article.FirstOrDefault().Sel_Price3);
                                break;
                            case Price.HolooSellNumber.Sel_Price4:
                                articlePrice = Convert.ToDecimal(article.FirstOrDefault().Sel_Price4);
                                break;
                            case Price.HolooSellNumber.Sel_Price5:
                                articlePrice = Convert.ToDecimal(article.FirstOrDefault().Sel_Price5);
                                break;
                            case Price.HolooSellNumber.Sel_Price6:
                                articlePrice = Convert.ToDecimal(article.FirstOrDefault().Sel_Price6);
                                break;
                            case Price.HolooSellNumber.Sel_Price7:
                                articlePrice = Convert.ToDecimal(article.FirstOrDefault().Sel_Price7);
                                break;
                            case Price.HolooSellNumber.Sel_Price8:
                                articlePrice = Convert.ToDecimal(article.FirstOrDefault().Sel_Price8);
                                break;
                            case Price.HolooSellNumber.Sel_Price9:
                                articlePrice = Convert.ToDecimal(article.FirstOrDefault().Sel_Price9);
                                break;
                            case Price.HolooSellNumber.Sel_Price10:
                                articlePrice = Convert.ToDecimal(article.FirstOrDefault().Sel_Price10);
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.LogCritical(e,
                            $"Product Prices ID : {productPrices.Id}, Article Code : {productPrices.ArticleCode}, Article Code Customer : {productPrices.ArticleCodeCustomer}");
                    }

                    if (articlePrice < 10)
                    {
                        //product.Prices.Remove(productPrices);
                        //tempPriceList.Add(productPrices);
                        continue;
                    }

                    productPrices.Amount = articlePrice / 10;
                    double soldExist = 0;
                    if ( productPrices.ArticleCode != null)
                    {
                        int userCode = Convert.ToInt32(_configuration.GetValue<string>("UserCode"));
                        soldExist = _aBailRepository.GetWithACode(userCode, productPrices.ArticleCode, cancellationToken);
                    }

                    productPrices.Exist = (double)article.Sum(x => x.Exist) - soldExist;

                    if (isCheckExist == true && productPrices.Exist == 0)
                    {
                        //product.Prices.Remove(productPrices);
                        //tempPriceList.Add(productPrices);
                        continue;
                    }
                    tempPriceList.Add(productPrices);
                }
            }
        }
        return tempPriceList;
    }
}