using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.Services.IServices;

public interface ICityService : IEntityService<City>
{
    Task<ServiceResult<List<City>>> Filtering(string filter, int id);
    Task<ServiceResult<List<City>>> Load(int id);
    Task<ServiceResult<List<City>>> GetWithPagination(string search = "", int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult> Add(City city);
    Task<ServiceResult> Edit(City city);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<List<City>>> LoadAllCity();
    Task<ServiceResult<City>> GetById(int id);
    Task<ServiceResult<City>> GetByStateId(int id);
}