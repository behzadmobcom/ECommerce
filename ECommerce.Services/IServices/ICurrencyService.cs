using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.Services.IServices;

public interface ICurrencyService : IEntityService<Currency>
{
    Task<ServiceResult<List<Currency>>> Filtering(string filter);
    Task<ServiceResult<List<Currency>>> Load(string search = "", int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult> Add(Currency currency);
    Task<ServiceResult> Edit(Currency currency);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<Currency>> GetById(int id);
}