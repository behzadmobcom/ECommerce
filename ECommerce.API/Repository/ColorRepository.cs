using API.DataContext;
using API.Interface;
using API.Utilities;
using Entities;
using Entities.Helper;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class ColorRepository : AsyncRepository<Color>, IColorRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public ColorRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<Color>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return PagedList<Color>.ToPagedList(
            await _context.Colors.Where(x => x.Name.Contains(paginationParameters.Search)).AsNoTracking()
                .OrderBy(on => on.Id).ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }

    public async Task<Color> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _context.Colors.Where(x => x.Name == name).FirstOrDefaultAsync(cancellationToken);
    }
}