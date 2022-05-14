using API.DataContext;
using API.Interface;
using API.Utilities;
using Entities;
using Entities.Helper;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class SupplierRepository : AsyncRepository<Supplier>, ISupplierRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public SupplierRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<Supplier>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return PagedList<Supplier>.ToPagedList(
            await _context.Suppliers.Where(x => x.Name.Contains(paginationParameters.Search)).AsNoTracking()
                .OrderBy(on => on.Id).ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }

    public async Task<Supplier> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _context.Suppliers.Where(x => x.Name == name).FirstOrDefaultAsync(cancellationToken);
    }
}