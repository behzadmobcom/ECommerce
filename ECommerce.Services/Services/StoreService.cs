

using Services.IServices;
using Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Helper;

namespace Services.Services
{
    public class StoreService : EntityService<Store>, IStoreService
    {
        private const string Url = "api/Store";
        private List<Store> _stores;
        private readonly IHttpService _http;


        public StoreService(IHttpService http) : base(http)
        {
            _http = http;
        }

        public async Task<ServiceResult<List<Store>>> Load(string search = "", int pageNumber = 0, int pageSize = 10)
        {
            var result = await ReadList(Url, $"Get?PageNumber={pageNumber}&PageSize={pageSize}&Search={search}");
            return Return(result);
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
        public async Task<ServiceResult<List<Store>>> Filtering(string filter)
        {
            if (_stores == null)
            {
                var brands = await Load();
                if (brands.Code > 0) return brands;
                _stores = brands.ReturnData;
            }

            var result = _stores.Where(x => x.Name.Contains(filter)).ToList();
            if (result.Count == 0)
            {
                return new ServiceResult<List<Store>> { Code = ServiceCode.Info, Message = "انبار یافت نشد" };
            }
            return new ServiceResult<List<Store>>
            {
                Code = ServiceCode.Success,
                ReturnData = result
            };
        }

        public async Task<ServiceResult> Add(Store store)
        {
            var result = await Create(Url, store);
            _stores = null;
            return Return(result);
        }

        public async Task<ServiceResult> Edit(Store store)
        {
            var result = await Update(Url, store);
            _stores = null;
            return Return(result);
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var result = await Delete(Url, id);
            _stores = null;
            return Return(result);
        }
        public async Task<ServiceResult<Store>> GetById(int id)
        {
            var result = await _http.GetAsync<Store>(Url, $"GetById?id={id}");
            return Return<Store>(result);
        }

    }
}
