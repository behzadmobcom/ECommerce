using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Services.IServices;

public interface ICartService
{
    Task<ServiceResult<List<PurchaseOrderViewModel>>> Load(HttpContext context, bool shouldUpdatePurchaseOrderDetails = false);
    Task<ServiceResult> Add(HttpContext context, int productId, int priceId, int count);
    Task<ServiceResult> Delete(HttpContext context, int id, int productId, int priceId, bool deleteFromCookie = false);
    Task<ServiceResult> Decrease(HttpContext context, int id, int productId, int priceId);
    Task<ServiceResult<List<PurchaseOrderViewModel>>> CartListFromServer(bool shouldUpdatePurchaseOrderDetails = false);
}