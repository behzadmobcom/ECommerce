using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repository;

public class ProductUserRankRepository : AsyncRepository<ProductUserRank>, IProductUserRankRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public ProductUserRankRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ProductUserRank?> GetByProductUser(int productId, int userId, CancellationToken cancellationToken)
    {
        return await _context.ProductUserRanks
            .Where(x => x.ProductId == productId && x.UserId == userId).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> GetBySumProduct(int productId, CancellationToken cancellationToken)
    {
        var sum = await _context.ProductUserRanks
            .Where(x => x.ProductId == productId).SumAsync(s => s.Stars,cancellationToken);

        var count = await _context.ProductUserRanks
            .Where(x => x.ProductId == productId).CountAsync(cancellationToken);

        return sum/ count;
    }
}