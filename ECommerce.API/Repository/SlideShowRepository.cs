using API.DataContext;
using API.Interface;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class SlideShowRepository : AsyncRepository<SlideShow>, ISlideShowRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public SlideShowRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public bool IsRepetitiveProduct(int id, CancellationToken cancellationToken)
    {
        return _context.SlideShows.Any(x => x.ProductId == id);
    }

    public async Task<SlideShow> GetByTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.SlideShows.Where(x => x.Title == title).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<SlideShow>> GetAllWithInclude(CancellationToken cancellationToken)
    {
        return await _context.SlideShows.Include(nameof(SlideShow.Product)).ToListAsync(cancellationToken);
    }
}