using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Interface;

public interface IProductCommentRepository : IAsyncRepository<ProductComment>
{
    Task<PagedList<ProductComment>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken);
    IQueryable<ProductComment> GetAllAccesptedComments(int productId,CancellationToken cancellationToken);
}