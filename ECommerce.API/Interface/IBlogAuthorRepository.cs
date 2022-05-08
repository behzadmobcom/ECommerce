using System.Threading;
using Entities;
using System.Threading.Tasks;
using API.Utilities;
using Entities.Helper;

namespace API.Interface
{
    public interface IBlogAuthorRepository : IAsyncRepository<BlogAuthor>
    {
        Task<PagedList<BlogAuthor>> Search(PaginationParameters paginationParameters,
            CancellationToken cancellationToken);
        Task<BlogAuthor> GetByName(string name, CancellationToken cancellationToken);
    }
}
