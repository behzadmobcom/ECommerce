using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Interface;

public interface IBlogAuthorRepository : IAsyncRepository<BlogAuthor>
{
    Task<PagedList<BlogAuthor>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken);

    Task<BlogAuthor> GetByName(string name, CancellationToken cancellationToken);
}