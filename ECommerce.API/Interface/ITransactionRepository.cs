using Ecommerce.Entities;
using ECommerce.API.Utilities;
using Ecommerce.Entities.Helper;

namespace ECommerce.API.Interface;

public interface ITransactionRepository : IAsyncRepository<Transaction>
{
    Task<PagedList<Transaction>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken);
}