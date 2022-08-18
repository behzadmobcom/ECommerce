using API.DataContext;
using API.Interface;
using API.Utilities;
using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class PriceRepository : AsyncRepository<Price>, IPriceRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public PriceRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<Price>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return PagedList<Price>.ToPagedList(
            await _context.Prices.Where(x => x.ProductId == Convert.ToInt32(paginationParameters.Search)).AsNoTracking()
                .Include(i => i.Color).OrderBy(on => on.Id).ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }

    public async Task<int> AddAll(IEnumerable<Price> prices, CancellationToken cancellationToken)
    {
        await _context.Prices.AddRangeAsync(prices, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> EditAll(IEnumerable<Price> prices, int id, CancellationToken cancellationToken)
    {
        _context.Prices.RemoveRange(_context.Prices.Where(x => x.ProductId == prices.FirstOrDefault().ProductId));
        foreach (var price in prices)
        {
            price.Id = 0;
            price.ProductId = id;
            await _context.Prices.AddAsync(price, cancellationToken);
        }

        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Price>> PriceOfProduct(int id, CancellationToken cancellationToken)
    {
        return await _context.Prices.AsNoTracking().Where(x => x.ProductId == id).Include(x => x.Currency).Include(c=>c.Color)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<ProductIndexPageViewModel?>> TopDiscounts(int count, CancellationToken cancellationToken)
    {
        var products = new List<ProductIndexPageViewModel?>();
        var discounts = _context.Discounts.OrderByDescending(x => x.Amount).Select(x => x.Prices).Take(count);
        var i = 0;
        foreach (var discount in discounts)
            foreach (var product in discount)
            {
                products.Add(await _context.Products
                    .Where(x => x.Id == product.Id && x.Images!.Count > 0 && x.Prices!.Any())
                    .Select(p => new ProductIndexPageViewModel
                    {
                        Prices = p.Prices!,
                        Alt = p.Images!.First().Alt,
                        Brand = p.Brand!.Name,
                        Name = p.Name,
                        Description = p.Description,
                        Id = p.Id,
                        ImagePath = $"{p.Images!.First().Path}/{p.Images!.First().Name}",
                        Stars = p.Star,
                        Url = p.Url
                    })
                    .FirstOrDefaultAsync(cancellationToken));
                i++;
                if (i == count) break;
            }

        //if (productIndexPageViewModel.Count < 5)
        //{
        //    productIndexPageViewModel.AddRange(await TopNew(count - productIndexPageViewModel.Count, cancellationToken));
        //}
        return products;
    }
}