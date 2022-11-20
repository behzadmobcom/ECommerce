using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.Services.IServices;

public interface ISupplierService : IEntityService<Supplier>
{
    Task<ServiceResult<List<Supplier>>> Filtering(string filter);
    Task<ServiceResult<List<Supplier>>> Load(string search = "", int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult<Dictionary<int, string>>> LoadDictionary();
    Task<ServiceResult> Add(Supplier supplier);
    Task<ServiceResult> Edit(Supplier supplier);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<Supplier>> GetById(int id);
}