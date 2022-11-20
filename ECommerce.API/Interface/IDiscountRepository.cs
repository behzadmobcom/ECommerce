using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;

namespace ECommerce.API.Interface;

public interface IDiscountRepository : IAsyncRepository<Discount>
{
    Task<PagedList<Discount>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);

    Task<Discount> GetByName(string name, CancellationToken cancellationToken);

    Task<Discount> GetByCode(string code, CancellationToken cancellationToken);

    Task<Discount?> GetLast(CancellationToken cancellationToken);

    Task<DiscountWithTimeViewModel> GetWithTime(CancellationToken cancellationToken);
}