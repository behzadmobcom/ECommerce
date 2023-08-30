using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Services.IServices;

public interface ICompareService
{
    ServiceResult Remove(HttpContext context, int productId);
    Task<ServiceResult<List<ProductCompareViewModel>>> CompareList(List<int> productId);
    Task<ServiceResult<List<ProductCompareViewModel>>> GetProductsByCategories(int categoryId);
}