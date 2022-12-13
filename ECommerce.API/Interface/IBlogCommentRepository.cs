using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Interface;

public interface IBlogCommentRepository : IAsyncRepository<BlogComment>
{
    Task<PagedList<BlogComment>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken);
    Task<PagedList<BlogComment>> GetAllAccesptedComments(PaginationParameters paginationParameters,
    CancellationToken cancellationToken);

}