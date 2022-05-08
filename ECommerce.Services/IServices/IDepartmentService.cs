using Entities;

using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Helper;

namespace Services.IServices
{
    public interface IDepartmentService : IEntityService<Department>
    {
        Task<ServiceResult<List<Department>>> Filtering(string filter);
        Task<ServiceResult<List<Department>>> Load(int pageNumber = 0, int pageSize = 10);
        Task<ServiceResult> Add(Department department);
        Task<ServiceResult> Edit(Department department);
        Task<ServiceResult> Delete(int id);
        Task<ServiceResult<Department>> GetById(int id);
    }
}
