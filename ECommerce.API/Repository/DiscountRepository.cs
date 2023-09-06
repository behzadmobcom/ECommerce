using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repository;

public class DiscountRepository : AsyncRepository<Discount>, IDiscountRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public DiscountRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<Discount>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return PagedList<Discount>.ToPagedList(
            await _context.Discounts.Where(x => x.Name.Contains(paginationParameters.Search)).AsNoTracking()
                .OrderBy(on => on.Id).ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }

    public async Task<Discount> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _context.Discounts.Where(x => x.Name == name).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Discount?> GetLast(CancellationToken cancellationToken)
    { 
        var result= await _context.Discounts.Include(i => i.Prices).ThenInclude(x=>x.Product).ThenInclude(y=>y.Images).OrderByDescending(o=>o.EndDate).FirstOrDefaultAsync(cancellationToken);
        if (result.Prices.Count() > 0) return result;
        return null;
    }

    public async Task<Discount> GetByCode(string code, CancellationToken cancellationToken)
    {
        return await _context.Discounts.Where(x => x.Code == code).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<DiscountWithTimeViewModel> GetWithTime(CancellationToken cancellationToken)
    {
        var discount = await _context.Discounts.Where(x => x.EndDate < DateTime.Now).Include(x => x.Prices)
            .FirstOrDefaultAsync(cancellationToken);
        var product = new Product();
        if (discount == null)
        {
            product = await _context.Products
                .Include(x => x.Images)
                .Include(x => x.Brand)
                .Include(x => x.Prices)
                .FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            //var temp = discount.Products.OrderByDescending(x => x.Prices.Max(x => x.Amount)).FirstOrDefault();
            var temp = discount.Prices.Select(x => x.Product).OrderByDescending(x => x.Prices.Max(y => y.Amount)).FirstOrDefault();
            product = await _context.Products
                .Where(x => x.Id == temp.Id)
                .Include(x => x.Images)
                .Include(x => x.Brand)
                .Include(x => x.Prices)
                .FirstOrDefaultAsync(cancellationToken);
        }

        var ret = new DiscountWithTimeViewModel
        {
            ProductId = product.Id,
            Alt = product.Images.FirstOrDefault()?.Alt,
            Brand = product.Brand.Name,
            Description = product.Description,
            EndDateTime = discount?.EndDate,
            Price = product.Prices.FirstOrDefault(x => !x.IsColleague && x.MinQuantity == 1).Amount,
            ImagePath = $"{product.Images.FirstOrDefault()?.Path}/{product.Images.FirstOrDefault()?.Name}",
            Name = product.Name,
            Url = product.Url
        };
        return ret;
    }

    public bool Active(int id)
    {
        var discount = _context.Discounts.Find(id);
        discount.IsActive = !discount.IsActive;
       Update(discount);
        return discount.IsActive;
    }
}