using Ecommerce.Entities;
using ECommerce.API.Utilities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;

namespace ECommerce.API.Interface;

public interface ITransactionRepository : IAsyncRepository<Transaction>
{
    Task<PagedList<Transaction>> Search(TransactionFiltreViewModel transactionFiltreViewModel,
        CancellationToken cancellationToken);
}