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
    public class CurrencyRepository : AsyncRepository<Currency>, ICurrencyRepository
    {
        private readonly SunflowerECommerceDbContext _context;
        public CurrencyRepository(SunflowerECommerceDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<PagedList<Currency>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken)
        {
            return PagedList<Currency>.ToPagedList(await _context.Currencies.Where(x => x.Name.Contains(paginationParameters.Search)).AsNoTracking().OrderBy(on => on.Id).ToListAsync(cancellationToken),
                paginationParameters.PageNumber,
                paginationParameters.PageSize);
        }
        public async Task<Currency> GetByName(string name, CancellationToken cancellationToken) => await _context.Currencies.Where(x => x.Name == name).FirstOrDefaultAsync(cancellationToken);
    }
}
