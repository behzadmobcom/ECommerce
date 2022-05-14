using Entities;
using Entities.Helper;

namespace Services.IServices;

public interface IStoreService : IEntityService<Store>
{
    Task<ServiceResult<List<Store>>> Filtering(string filter);
    Task<ServiceResult<List<Store>>> Load(string search = "", int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult<Dictionary<int, string>>> LoadDictionary();
    Task<ServiceResult> Add(Store store);
    Task<ServiceResult> Edit(Store store);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<Store>> GetById(int id);
}