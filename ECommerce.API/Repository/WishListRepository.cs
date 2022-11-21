using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repository;

public class WishListRepository : AsyncRepository<WishList>, IWishListRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public WishListRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<WishList> GetByProductUser(int productId, int userId, CancellationToken cancellationToken)
    {
        return _context.WishLists
            .Where(x => x.ProductId == productId && x.UserId == userId).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<WishListViewModel>> GetByIdWithInclude(int userId, CancellationToken cancellationToken)
    {
        return await _context.WishLists.Where(x => x.UserId == userId)
            .Include(x => x.Product)
            .Include(x => x.Product.Brand)
            .Include(x => x.Product.Images)
            .Include(x => x.Product.Prices)
            .Select(p => new WishListViewModel
            {
                Id = p.Id,
                ProductId = p.ProductId,
                Url = p.Product.Url,
                Name = p.Product.Name,
                Price = p.Product.Prices.FirstOrDefault(y => !y.IsColleague && y.MinQuantity == 1).Amount,
                ImagePath = $"{p.Product.Images.FirstOrDefault().Path}/{p.Product.Images.FirstOrDefault().Name}",
                Brand = p.Product.Brand.Name,
                Alt = p.Product.Images.FirstOrDefault().Alt,
                PriceId= p.Product.Prices.FirstOrDefault(y => !y.IsColleague && y.MinQuantity == 1).Id
                //Exist = p.Product.Exist,
                //StoreStatus = p.Product.Exist >0 ? "در انبار":"تمام شد"
            })
            .ToListAsync(cancellationToken);
    }
}