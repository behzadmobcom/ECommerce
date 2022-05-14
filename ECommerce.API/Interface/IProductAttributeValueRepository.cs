using API.Utilities;
using Entities;
using Entities.Helper;

namespace API.Interface;

public interface IProductAttributeValueRepository : IAsyncRepository<ProductAttributeValue>
{
    Task<PagedList<ProductAttributeValue>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken);
}