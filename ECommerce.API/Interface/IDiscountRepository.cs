using API.Utilities;
using Entities;
using Entities.Helper;
using Entities.ViewModel;

namespace API.Interface;

public interface IDiscountRepository : IAsyncRepository<Discount>
{
    Task<PagedList<Discount>> Search(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    Task<Discount> GetByName(string name, CancellationToken cancellationToken);

    Task<Discount> GetByCode(string code, CancellationToken cancellationToken);

    Task<DiscountWithTimeViewModel> GetWithTime(CancellationToken cancellationToken);
}