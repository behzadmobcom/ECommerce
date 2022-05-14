using Entities;
using Entities.Helper;

namespace Services.IServices;

public interface ICityService : IEntityService<City>
{
    Task<ServiceResult<List<City>>> Filtering(string filter, int id);
    Task<ServiceResult<List<City>>> Load(int id);
    Task<ServiceResult> Add(City city);
    Task<ServiceResult> Edit(City city);
    Task<ServiceResult> Delete(int id);
}