using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Services.IServices;

public interface ICompareService
{
    ServiceResult Remove(HttpContext context, int productId);
    //ServiceResult<List<int>> Load(HttpContext context);
    Task<ServiceResult<List<ProductCompareViewModel>>> CompareList(List<int> productListId);
    Task<ServiceResult<List<ProductCompareViewModel>>> GetProductsByCategories(List<int> categoryId);
}