using Ecommerce.Entities;
using Ecommerce.Entities.ViewModel;

namespace ECommerce.API.Interface;

public interface IWishListRepository : IAsyncRepository<WishList>
{
    Task<WishList> GetByProductUser(int productId, int userId, CancellationToken cancellationToken);
    Task<List<WishListViewModel>> GetByIdWithInclude(int userId, CancellationToken cancellationToken);
}