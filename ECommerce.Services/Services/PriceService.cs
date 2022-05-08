

using Services.IServices;
using Entities;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Helper;

namespace Services.Services
{
    public class PriceService : EntityService<Price>, IPriceService
    {
        private const string Url = "api/Prices";
        private List<Price> _price;

        public PriceService(IHttpService http) : base(http)
        {
        }

        public async Task<ServiceResult<List<Price>>> Load(string search = "", int pageNumber = 0, int pageSize = 10)
        {
            var result = await ReadList(Url, $"Get?PageNumber={pageNumber}&PageSize={pageSize}&Search={search}");
            return Return<List<Price>>(result);
        }
      

        public async Task<ServiceResult> Add(Price price)
        {
            var result = await Create(Url, price);
            _price = null;
            return Return(result);
        }

        public async Task<ServiceResult> Edit(Price brand)
        {
            var result = await Update(Url, brand);
            _price = null;
            return Return(result);
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var result = await Delete(Url, id);
            _price = null;
            return Return(result);
        }
        public async Task<ServiceResult<List<Price>>> PriceOfProduct(int productId)
        {
            var result = await ReadList(Url,$"Product/{productId}");
            return Return<List<Price>>(result);
        }
    }
}
