using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Interface;

public interface IImageRepository : IAsyncRepository<Image>
{
    Task<PagedList<Image>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    Task<int> DeleteByName(string name, CancellationToken cancellationToken);
    Task<List<Image>> GetByProductId(int productId, CancellationToken cancellationToken);
    Task<Image> GetByBlogId(int blogId, CancellationToken cancellationToken);
}