using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.Services.IServices;

public interface ITransactionService : IEntityService<Transaction>
{
    Task<ServiceResult<List<Transaction>>> GetAll();
    Task<ServiceResult<List<Transaction>>> Load(string search = "", int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult> Add(Transaction tag);
    Task<ServiceResult> Edit(Transaction tag);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<Transaction>> GetById(int id);
}