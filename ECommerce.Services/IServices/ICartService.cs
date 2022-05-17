using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Http;

namespace Services.IServices;

public interface ICartService
{
    Task<ServiceResult<List<PurchaseOrderViewModel>>> Load(HttpContext context);
    Task<ServiceResult> Add(HttpContext context, int productId, int priceId);
    Task<ServiceResult> Delete(HttpContext context, int id, int productId, int priceId);
    Task<ServiceResult> Decrease(HttpContext context, int id, int productId, int priceId);
    Task<Guid> PreFactor(int orderId,string refId,int amount);
}