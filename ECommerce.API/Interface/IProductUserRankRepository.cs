using Ecommerce.Entities;

namespace ECommerce.API.Interface;

public interface IProductUserRankRepository : IAsyncRepository<ProductUserRank>
{
    Task<ProductUserRank?> GetByProductUser(int productId, int userId, CancellationToken cancellationToken);

    Task<double> GetBySumProduct(int productId, CancellationToken cancellationToken);
}