using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.Services.IServices;

public interface ISizeService : IEntityService<Size>
{
    Task<ServiceResult<List<Size>>> Filtering(string filter);
    Task<ServiceResult<List<Size>>> Load(string search = "", int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult> Add(Size size);
    Task<ServiceResult> Edit(Size size);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<Size>> GetById(int id);
}