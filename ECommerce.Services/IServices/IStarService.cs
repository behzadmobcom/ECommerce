using Entities;
using Entities.Helper;

namespace ECommerce.Services.IServices;

public interface IStarService
{
    public int FillStars { get; set; }
    public int EmptyStars { get; set; }
    public int StarCount { get; set; }
    Task<ApiResult<List<ProductUserRank>>> Load();
    Task<string> SaveStars(int productId, int starNumber, string productUrl);
    Task<int> SumStarsByProductId(int productId);
}