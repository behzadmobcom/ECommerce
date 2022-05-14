using API.DataContext;
using API.Interface;
using API.Utilities;
using Entities;
using Entities.Helper;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class ImageRepository : AsyncRepository<Image>, IImageRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public ImageRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<Image>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return PagedList<Image>.ToPagedList(
            await _context.Images.Where(x => x.Alt.Contains(paginationParameters.Search)).AsNoTracking()
                .OrderBy(on => on.Id).ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }

    public async Task<int> DeleteByName(string name, CancellationToken cancellationToken)
    {
        var image = await _context.Images.Where(x => x.Name == name).FirstOrDefaultAsync(cancellationToken);
        _context.Images.Remove(image);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Image>> GetByProductId(int productId, CancellationToken cancellationToken)
    {
        return await
            _context.Images.Where(x => x.ProductId == productId).ToListAsync(cancellationToken);
    }
}