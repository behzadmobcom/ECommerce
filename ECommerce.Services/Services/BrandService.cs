using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Helper;
using Services.IServices;

namespace Services.Services
{
    public class BrandService : EntityService<Brand>, IBrandService
    {
        private const string Url = "api/Brands";
        private List<Brand> _brands;
        private readonly IHttpService _http;

        public BrandService(IHttpService http) : base(http)
        {
            _http = http;
        }

        public async Task<ServiceResult<List<Brand>>> Load()
        {
            var result = await ReadList(Url, $"GetAll");
            return Return<List<Brand>>(result);
        }
        public async Task<ServiceResult<List<Brand>>> GetAll(string search = "", int pageNumber = 0, int pageSize = 10)
        {
            var result = await ReadList(Url, $"GetAllWithPagination?PageNumber={pageNumber}&Search={search}&PageSize={pageSize}");
            return Return<List<Brand>>(result);
        }
        public async Task<ServiceResult<Dictionary<int, string>>> LoadDictionary()
        {
            var result = await ReadList(Url, "GetAll");
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
        public async Task<ServiceResult<List<Brand>>> Filtering(string filter)
        {
            if (_brands == null)
            {
                var brands = await Load();
                if (brands.Code > 0) return brands;
                _brands = brands.ReturnData;
            }

            var result = _brands.Where(x => x.Name.Contains(filter)).ToList();
            if (result.Count == 0)
            {
                return new ServiceResult<List<Brand>> {Code = ServiceCode.Info, Message = "برندی یافت نشد"};
            }
            return new ServiceResult<List<Brand>>
            {
                Code = ServiceCode.Success,
                ReturnData= result
            };
        }

        public async Task<ServiceResult> Add(Brand brand)
        {
            var result = await Create(Url, brand);
            _brands = null;
            return Return(result);
        }

        public async Task<ServiceResult> Edit(Brand brand)
        {
            var result = await Update(Url, brand);
            _brands = null;
            return Return(result);
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var result = await Delete(Url, id);
            _brands = null;
            return Return(result);
        }

        public async Task<ServiceResult<Brand>> GetById(int id)
        {
            var result = await _http.GetAsync<Brand>(Url,$"GetById?id={id}");
            return Return<Brand>(result);
        }
    }
}
