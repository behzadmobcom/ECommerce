using System.Threading;
using System.Threading.Tasks;
using API.Utilities;
using Entities;
using Entities.Helper;

namespace API.Interface
{
    public interface IMessageRepository : IAsyncRepository<Message>
    {
        Task<PagedList<Message>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    }
}
