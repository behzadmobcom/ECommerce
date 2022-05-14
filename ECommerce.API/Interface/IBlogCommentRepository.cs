using API.Utilities;
using Entities;
using Entities.Helper;

namespace API.Interface;

public interface IBlogCommentRepository : IAsyncRepository<BlogComment>
{
    Task<PagedList<BlogComment>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken);
}