using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Interface;

public interface IProductAttributeValueRepository : IAsyncRepository<ProductAttributeValue>
{
    Task<PagedList<ProductAttributeValue>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken);
}