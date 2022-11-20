using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.Services.IServices;

public interface IDepartmentService : IEntityService<Department>
{
    Task<ServiceResult<List<Department>>> Filtering(string filter);
    Task<ServiceResult<List<Department>>> Load(int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult> Add(Department department);
    Task<ServiceResult> Edit(Department department);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<Department>> GetById(int id);
    Task<ServiceResult<List<Department>>> GetAll();
}