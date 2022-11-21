using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.Services.IServices;

public interface IEmployeeService : IEntityService<Employee>
{
    Task<ServiceResult<List<Employee>>> Load(string search = "", int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult<List<Employee>>> GetAll(string search = "", int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult<Dictionary<int, string>>> LoadDictionary();
    Task<ServiceResult> Add(Employee employee);
    Task<ServiceResult> Edit(Employee employee);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<Employee>> GetById(int id);
}