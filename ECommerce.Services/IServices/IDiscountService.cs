using Entities;

using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Helper;

namespace Services.IServices
{
    public interface IDiscountService : IEntityService<Discount>
    {
        Task<ServiceResult<List<Discount>>> Filtering(string filter);
        Task<ServiceResult<List<Discount>>> Load(string search = "", int pageNumber = 0, int pageSize = 10);
        Task<ServiceResult<Dictionary<int, string>>> LoadDictionary();
        Task<ServiceResult> Add(Discount discount);
        Task<ServiceResult> Edit(Discount discount);
        Task<ServiceResult> Delete(int id);
        Task<ServiceResult<Discount>> GetById(int id);
    }
}
