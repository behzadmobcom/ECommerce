using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.Services.IServices;

public interface IBrandService : IEntityService<Brand>
{
    Task<ServiceResult<List<Brand>>> Load();
    Task<ServiceResult<List<Brand>>> GetAll(string search = "", int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult<Dictionary<int, string>>> LoadDictionary();
    Task<ServiceResult> Add(Brand brand);
    Task<ServiceResult> Edit(Brand brand);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<Brand>> GetById(int id);
}