using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.Services.IServices;

public interface IUnitService : IEntityService<Unit>
{
    Task<ServiceResult<List<Unit>>> Filtering(string filter);
    Task<ServiceResult<List<Unit>>> Load(string search = "", int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult> Add(Unit unit);
    Task<ServiceResult> Edit(Unit unit);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult> ConvertHolooUnits();
    Task<ServiceResult<Unit>> GetById(int id);
}