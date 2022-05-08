using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.DataContext;
using API.Interface;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ProductUserRankRepository : AsyncRepository<ProductUserRank>, IProductUserRankRepository
    {
        private readonly SunflowerECommerceDbContext _context;
        public ProductUserRankRepository(SunflowerECommerceDbContext context) : base(context)
        {
            _context = context;
        }
        public Task<ProductUserRank> GetByProductUser(int productId, int userId, CancellationToken cancellationToken) => _context.ProductUserRanks
            .Where(x => x.ProductId == productId && x.UserId == userId).FirstOrDefaultAsync(cancellationToken);
    }
}
