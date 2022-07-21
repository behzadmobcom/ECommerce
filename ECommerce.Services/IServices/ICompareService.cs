using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Services.IServices;

public interface ICompareService
{
    ServiceResult<int> Add(HttpContext context, int productId);
    ServiceResult Remove(HttpContext context, int productId);
    ServiceResult<List<int>> Load(HttpContext context);
    Task<ServiceResult<List<ProductCompareViewModel>>> CompareList(HttpContext context);
}