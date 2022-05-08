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
    public class MessageRepository : AsyncRepository<Message>, IMessageRepository
    {
        private readonly SunflowerECommerceDbContext _context;
        public MessageRepository(SunflowerECommerceDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<PagedList<Message>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken)
        {
            return PagedList<Message>.ToPagedList(await _context.Messages.Where(x => x.Name.Contains(paginationParameters.Search)).AsNoTracking().OrderBy(on => on.Id).ToListAsync(cancellationToken),
                paginationParameters.PageNumber,
                paginationParameters.PageSize);
        }
    }
}
