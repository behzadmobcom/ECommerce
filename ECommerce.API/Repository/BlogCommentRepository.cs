using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repository;

public class BlogCommentRepository : AsyncRepository<BlogComment>, IBlogCommentRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public BlogCommentRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<BlogComment>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return PagedList<BlogComment>.ToPagedList(
            await _context.BlogComments.Where(x => x.Name.Contains(paginationParameters.Search) && x.BlogId != null).Include(x=>x.Blog).AsNoTracking()
                .OrderByDescending(on => on.Id).ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }

    public async Task<PagedList<BlogComment>> GetAllAccesptedComments(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return PagedList<BlogComment>.ToPagedList(
            await _context.BlogComments.Where(x => x.IsAccepted == true && x.BlogId == System.Convert.ToInt32(paginationParameters.Search))
            .AsNoTracking().OrderByDescending(on => on.Id).Include(x => x.Answer).ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }
}