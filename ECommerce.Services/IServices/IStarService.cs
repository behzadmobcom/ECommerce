using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.Services.IServices;

public interface IStarService
{
    public int FillStars { get; set; }
    public int EmptyStars { get; set; }
    public int StarCount { get; set; }
    Task<ApiResult<List<ProductUserRank>>> Load();
    Task<ServiceResult> SaveStars(int productId, int starNumber);
    Task<double> SumStarsByProductId(int productId);
}