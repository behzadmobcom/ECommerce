using API.DataContext;
using API.Interface;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Utilities;
using Entities.Helper;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ProductAttributeValueRepository : AsyncRepository<ProductAttributeValue>, IProductAttributeValueRepository
    {
        private readonly SunflowerECommerceDbContext _context;
        public ProductAttributeValueRepository(SunflowerECommerceDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<PagedList<ProductAttributeValue>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken)
        {
            return PagedList<ProductAttributeValue>.ToPagedList(await _context.ProductAttributeValues.Where(x => x.Value.Contains(paginationParameters.Search)).AsNoTracking().OrderBy(on => on.Id).ToListAsync(cancellationToken),
                paginationParameters.PageNumber,
                paginationParameters.PageSize);
        }
        public async Task<IEnumerable<ProductAttributeValue>> GetAll(int productAttributeId)
        {
            return await _context.ProductAttributeValues.Where(x => x.ProductAttributeId == productAttributeId).ToListAsync();
        }
    }
}
