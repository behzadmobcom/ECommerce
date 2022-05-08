using System.Threading;
using System.Threading.Tasks;
using API.Utilities;
using Entities;
using Entities.Helper;

namespace API.Interface
{
    public interface IProductCommentRepository : IAsyncRepository<ProductComment>
    {
        Task<PagedList<ProductComment>> Search(PaginationParameters paginationParameters,
            CancellationToken cancellationToken);
    }
}
