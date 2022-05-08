using API.DataContext;
using API.Interface;
using Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Utilities;
using Entities.Helper;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class PriceRepository : AsyncRepository<Price>, IPriceRepository
    {
        private readonly SunflowerECommerceDbContext _context;
        public PriceRepository(SunflowerECommerceDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<PagedList<Price>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken)
        {
            return PagedList<Price>.ToPagedList(await _context.Prices.Where(x => x.ProductId ==Convert.ToInt32(paginationParameters.Search)).AsNoTracking().Include(i=>i.Color).OrderBy(on => on.Id).ToListAsync(cancellationToken),
                paginationParameters.PageNumber,
                paginationParameters.PageSize);
        }
        public async Task<int> AddAll(IEnumerable<Price> prices, CancellationToken cancellationToken)
        {
            await _context.Prices.AddRangeAsync(prices, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> EditAll(IEnumerable<Price> prices, int id, CancellationToken cancellationToken)
        {
            _context.Prices.RemoveRange(_context.Prices.Where(x => x.ProductId == prices.FirstOrDefault().ProductId));
            foreach (var price in prices)
            {
                price.Id = 0;
                price.ProductId = id;
                await _context.Prices.AddAsync(price, cancellationToken);
            }
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Price>> PriceOfProduct(int id, CancellationToken cancellationToken)
        {
            return await _context.Prices.AsNoTracking().Where(x => x.ProductId == id).Include(x => x.Currency).ToListAsync(cancellationToken);
        }

    }
}
