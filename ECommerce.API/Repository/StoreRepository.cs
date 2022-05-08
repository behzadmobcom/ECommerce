using API.DataContext;
using API.Interface;
using Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Utilities;
using Entities.Helper;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class StoreRepository : AsyncRepository<Store>, IStoreRepository
    {
        private readonly SunflowerECommerceDbContext _context;
        public StoreRepository(SunflowerECommerceDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<PagedList<Store>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken)
        {
            return PagedList<Store>.ToPagedList(await _context.Stores.Where(x => x.Name.Contains(paginationParameters.Search)).AsNoTracking().OrderBy(on => on.Id).ToListAsync(cancellationToken),
                paginationParameters.PageNumber,
                paginationParameters.PageSize);
        }
        public async Task<Store> GetByName(string name, CancellationToken cancellationToken) => await _context.Stores.Where(x => x.Name == name).FirstOrDefaultAsync(cancellationToken);
    }
}
