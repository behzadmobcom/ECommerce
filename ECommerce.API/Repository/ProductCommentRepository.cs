using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repository;

public class ProductCommentRepository : AsyncRepository<ProductComment>, IProductCommentRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public ProductCommentRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<ProductComment>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return PagedList<ProductComment>.ToPagedList(
            await _context.ProductComments
            .AsNoTracking().OrderByDescending(on => on.Id).Include(x=>x.Product).ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }

    public IQueryable<ProductComment> GetAllAccesptedComments(int productId , CancellationToken cancellationToken)
    {        
        var result =  _context.ProductComments.Where(x => x.IsAccepted == true && x.ProductId==productId).AsQueryable();
        return result;
    }
    
}