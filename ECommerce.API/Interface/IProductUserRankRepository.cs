using System.Threading;
using System.Threading.Tasks;
using Entities;

namespace API.Interface
{
    public interface IProductUserRankRepository : IAsyncRepository<ProductUserRank>
    {
        Task<ProductUserRank> GetByProductUser(int productId,int userId, CancellationToken cancellationToken);
    }
}
