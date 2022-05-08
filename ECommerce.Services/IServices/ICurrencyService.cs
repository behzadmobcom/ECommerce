using Entities;

using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Helper;

namespace Services.IServices
{
    public interface ICurrencyService : IEntityService<Currency>
    {
        Task<ServiceResult<List<Currency>>> Filtering(string filter);
        Task<ServiceResult<List<Currency>>> Load(int pageNumber = 0, int pageSize = 10);
        Task<ServiceResult> Add(Currency currency);
        Task<ServiceResult> Edit(Currency currency);
        Task<ServiceResult> Delete(int id);
        Task<ServiceResult<Currency>> GetById(int id);
    }
}
