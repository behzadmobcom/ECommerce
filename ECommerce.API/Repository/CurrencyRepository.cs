using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repository;

public class CurrencyRepository : AsyncRepository<Currency>, ICurrencyRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public CurrencyRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<Currency>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return PagedList<Currency>.ToPagedList(
            await _context.Currencies.Where(x => x.Name.Contains(paginationParameters.Search)).AsNoTracking()
                .OrderBy(on => on.Id).ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }

    public async Task<Currency> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _context.Currencies.Where(x => x.Name == name).FirstOrDefaultAsync(cancellationToken);
    }
}