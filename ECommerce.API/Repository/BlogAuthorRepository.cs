using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repository;

public class BlogAuthorRepository : AsyncRepository<BlogAuthor>, IBlogAuthorRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public BlogAuthorRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<BlogAuthor>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return PagedList<BlogAuthor>.ToPagedList(
            await _context.BlogAuthors.Where(x => x.Name.Contains(paginationParameters.Search)).AsNoTracking()
                .OrderBy(on => on.Id).ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }

    public async Task<BlogAuthor> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _context.BlogAuthors.Where(x => x.Name == name).FirstOrDefaultAsync(cancellationToken);
    }
}