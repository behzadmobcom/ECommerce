using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;

namespace ECommerce.Services.IServices;

public interface IPriceService : IEntityService<Price>
{
    Task<ServiceResult<List<Price>>> Load(string search = "", int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult> Add(Price price);
    Task<ServiceResult> Edit(Price price);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<List<Price>>> PriceOfProduct(int productId);
    Task<ServiceResult<List<PriceViewModel>>> GetAllByViewModel();
}