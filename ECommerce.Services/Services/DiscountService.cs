

using Services.IServices;
using Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Helper;

namespace Services.Services
{
    public class DiscountService : EntityService<Discount>, IDiscountService
    {
        private const string Url = "api/Discounts";
        private List<Discount> _discount;
        private readonly IHttpService _http;

        public DiscountService(IHttpService http) : base(http)
        {
            _http = http;
        }
        public async Task<ServiceResult<List<Discount>>> Load(string search = "", int pageNumber = 0, int pageSize = 10)
        {
            var result = await ReadList(Url, $"Get?PageNumber={pageNumber}&PageSize={pageSize}&Search={search}");
            return Return<List<Discount>>(result);
        }
        public async Task<ServiceResult<Dictionary<int, string>>> LoadDictionary()
        {
            var result = await ReadList(Url);
            if (result.Code == ResultCode.Success)
                return new ServiceResult<Dictionary<int, string>>
                {
                    Code = ServiceCode.Success,
                    ReturnData = result.ReturnData.ToDictionary(item => item.Id, item => item.Name),
                    Message = result.Messages?.FirstOrDefault()
                };
            return new ServiceResult<Dictionary<int, string>>
            {
                Code = ServiceCode.Error,
                Message = result.GetBody()
            };
        }
        public async Task<ServiceResult<List<Discount>>> Filtering(string filter)
        {
            if (_discount == null)
            {
                var discount = await Load();
                if (discount.Code > 0) return discount;
                _discount = discount.ReturnData;
            }
            var result = _discount.Where(x => x.Name.Contains(filter)).ToList();
            if (result.Count == 0)
            {
                return new ServiceResult<List<Discount>> { Code = ServiceCode.Info, Message = "تخفیفی یافت نشد" };
            }
            return new ServiceResult<List<Discount>>
            {
                Code = ServiceCode.Success,
                ReturnData = result
            };
        }

        public async Task<ServiceResult> Add(Discount discount)
        {
            var result = await Create(Url, discount);
            _discount = null;
            return Return(result);
        }

        public async Task<ServiceResult> Edit(Discount discount)
        {
            var result = await Update(Url, discount);
            _discount = null;
            return Return(result);
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var result = await Delete(Url, id);
            _discount = null;
            return Return(result);
        }
        public async Task<ServiceResult<Discount>> GetById(int id)
        {
            var result = await _http.GetAsync<Discount>(Url, $"GetById?id={id}");
            return Return<Discount>(result);
        }
    }
}
