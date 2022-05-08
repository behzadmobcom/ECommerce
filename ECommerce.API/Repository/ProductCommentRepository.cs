using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.DataContext;
using API.Interface;
using API.Utilities;
using Entities;
using Entities.Helper;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ProductCommentRepository : AsyncRepository<ProductComment>, IProductCommentRepository
    {
        private readonly SunflowerECommerceDbContext _context;
        public ProductCommentRepository(SunflowerECommerceDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<PagedList<ProductComment>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken)
        {
            return PagedList<ProductComment>.ToPagedList(await _context.ProductComments.Where(x => x.ProductId == Convert.ToInt32(paginationParameters.Search)).AsNoTracking().OrderBy(on => on.Id).ToListAsync(cancellationToken),
                paginationParameters.PageNumber,
                paginationParameters.PageSize);
        }
    }
}
