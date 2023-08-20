using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;
using ECommerce.API.Utilities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Repository;

public class StateRepository : AsyncRepository<State>, IStateRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public StateRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<State> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _context.States.Where(x => x.Name == name).FirstOrDefaultAsync(cancellationToken);
    }
    public async Task<PagedList<State>> Search(PaginationParameters paginationParameters,
      CancellationToken cancellationToken)
    {
        return PagedList<State>.ToPagedList(
            await _context.States.Where(x => x.Name.Contains(paginationParameters.Search)).AsNoTracking()
                .OrderBy(on => on.Id).ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }
}