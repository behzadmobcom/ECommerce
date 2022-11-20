using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;

namespace ECommerce.API.Interface;

public interface IPriceRepository : IAsyncRepository<Price>
{
    Task<PagedList<Price>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    Task<int> AddAll(IEnumerable<Price> prices, CancellationToken cancellationToken);

    Task<int> EditAll(IEnumerable<Price> prices, int id, CancellationToken cancellationToken);

    Task<IEnumerable<Price>> PriceOfProduct(int id, CancellationToken cancellationToken);

    Task<List<ProductIndexPageViewModel?>> TopDiscounts(int count, CancellationToken cancellationToken);
}