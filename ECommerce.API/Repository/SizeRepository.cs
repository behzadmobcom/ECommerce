using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repository;

public class SizeRepository : AsyncRepository<Size>, ISizeRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public SizeRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<Size>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return PagedList<Size>.ToPagedList(
            await _context.Sizes.Where(x => x.Name.Contains(paginationParameters.Search)).AsNoTracking()
                .OrderBy(on => on.Id).ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }

    public async Task<Size> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _context.Sizes.Where(x => x.Name == name).FirstOrDefaultAsync(cancellationToken);
    }
}