using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.Services.IServices;

public interface ITransactionService : IEntityService<Transaction>
{
    Task<ServiceResult<List<Transaction>>> GetAll();
    Task<ServiceResult<List<Transaction>>> Load(int userId = 0, string userName = "", string search = "", int pageNumber = 1, int pageSize = 10,
    string message = null, string code = null, decimal? minimumAmount = null, decimal? maximumAmount = null,
    Status status = Ecommerce.Entities.Status.New, PurchaseSort purchaseSort = PurchaseSort.HighToLowDateBuying);
    Task<ServiceResult> Add(Transaction tag);
    Task<ServiceResult> Edit(Transaction tag);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<Transaction>> GetById(int id);
}