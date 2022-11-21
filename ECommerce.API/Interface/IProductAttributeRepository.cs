using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Interface;

public interface IProductAttributeRepository : IAsyncRepository<ProductAttribute>
{
    Task<PagedList<ProductAttribute>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken);

    Task<ProductAttribute> GetByTitle(string title, CancellationToken cancellationToken);
    Task<IEnumerable<ProductAttribute>> GetAll(int productId, CancellationToken cancellationToken);

    Task<IEnumerable<ProductAttribute>> GetAllAttributeWithGroupId(int groupId, int productId,
        CancellationToken cancellationToken);
}