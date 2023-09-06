using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.Services.IServices;

public interface IDiscountService : IEntityService<Discount>
{
    Task<ServiceResult<List<Discount>>> Load(string search = "", int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult> Add(Discount discount);
    Task<ServiceResult> Edit(Discount discount);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<Discount>> GetById(int id);
    Task<ServiceResult<Discount>> GetLast();
    Task<bool> Activate(int discountId);
}