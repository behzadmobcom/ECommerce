using System;
using System.Collections.Generic;
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
    public class BlogAuthorRepository : AsyncRepository<BlogAuthor>, IBlogAuthorRepository
    {
        private readonly SunflowerECommerceDbContext _context;
        public BlogAuthorRepository(SunflowerECommerceDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<PagedList<BlogAuthor>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken)
        {
            return PagedList<BlogAuthor>.ToPagedList(await _context.BlogAuthors.Where(x => x.Name.Contains(paginationParameters.Search)).AsNoTracking().OrderBy(on => on.Id).ToListAsync(cancellationToken),
                paginationParameters.PageNumber,
                paginationParameters.PageSize);
        }

        public async Task<BlogAuthor> GetByName(string name, CancellationToken cancellationToken) => await _context.BlogAuthors.Where(x => x.Name == name).FirstOrDefaultAsync(cancellationToken);
    }
}
