using API.DataContext;
using API.Interface;
using API.Utilities;
using Entities;
using Entities.Helper;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class BlogCategoryRepository : AsyncRepository<BlogCategory>, IBlogCategoryRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public BlogCategoryRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<PagedList<BlogCategory>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return PagedList<BlogCategory>.ToPagedList(
            await _context.BlogCategories.Where(x => x.Name.Contains(paginationParameters.Search)).AsNoTracking()
                .OrderBy(on => on.Id).ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }

    public async Task<BlogCategory> GetByName(string name, int? parentId, CancellationToken cancellationToken)
    {
        return await _context.BlogCategories.Where(x => x.Name == name && x.Parent.Id == parentId)
            .FirstOrDefaultAsync(cancellationToken);
    }
}