using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repository;

public class SlideShowRepository : AsyncRepository<SlideShow>, ISlideShowRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public SlideShowRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public bool IsRepetitiveProduct(int id, int? productId, int? categoryId, CancellationToken cancellationToken)
    {
        var repetitive = true;
        repetitive = id == 0 ? _context.SlideShows.Any(x => x.ProductId == productId) : _context.SlideShows.Any(x => x.ProductId == productId && x.Id != id);
        return repetitive;
    }

    public async Task<SlideShow> GetByTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.SlideShows.Where(x => x.Title == title).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<SlideShow>> GetAllWithInclude(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _context.SlideShows
            .Include(c => c.Category)
            .Include(x => x.Product)
            .ThenInclude(p => p.Prices)
            .ThenInclude(x => x.Discount)
            .OrderBy(x => x.DisplayOrder)
            .Skip((pageNumber-1)* pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}