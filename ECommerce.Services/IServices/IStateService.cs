using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.Services.IServices;

public interface IStateService : IEntityService<State>
{
    Task<ServiceResult<List<State>>> Filtering(string filter, int id);
    Task<ServiceResult<List<State>>> Load();
    Task<ServiceResult<List<State>>> GetWithPagination(string search = "", int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult> Add(State state);
    Task<ServiceResult> Edit(State state);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<List<State>>> GetAll();
    Task<ServiceResult<State>> GetById(int id);
}