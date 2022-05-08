using System.Collections.Generic;
using Entities;

using System.Threading.Tasks;
using Entities.Helper;

namespace Services.IServices
{
    public interface IStarService
    {
        Task<ApiResult<List<ProductUserRank>>> Load();
        public int FillStars { get; set; }
        public int EmptyStars { get; set; }
        public int StarCount { get; set; }
        Task<string> SaveStars(int productId, int starNumber, string productUrl);
        Task<int> SumStarsByProductId(int productId);
    }
}
