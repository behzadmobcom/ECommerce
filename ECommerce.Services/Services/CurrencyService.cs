

using Services.IServices;
using Entities;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Helper;

namespace Services.Services
{
    public class CurrencyService : EntityService<Currency>, ICurrencyService
    {
        private const string Url = "api/Currencies";
        private List<Currency> _currencies;
        private readonly IHttpService _http;


        public CurrencyService(IHttpService http) : base(http)
        {
            _http = http;
        }

        public async Task<ServiceResult<List<Currency>>> Load(int pageNumber = 0, int pageSize = 10)
        {
            var result = await ReadList(Url, $"Get?PageNumber={pageNumber}&PageSize={pageSize}");
            return Return<List<Currency>>(result);
        }
        public async Task<ServiceResult<List<Currency>>> Filtering(string filter)
        {
            if (_currencies == null)
            {
                var currencies = await Load();
                if (currencies.Code > 0) return currencies;
                _currencies = currencies.ReturnData;
            }

            var result = _currencies.Where(x => x.Name.Contains(filter)).ToList();
            if (result.Count == 0)
            {
                return new ServiceResult<List<Currency>> { Code = ServiceCode.Info, Message = "ارزی یافت نشد" };
            }
            return new ServiceResult<List<Currency>>
            {
                Code = ServiceCode.Success,
                ReturnData = result
            };
        }

        public async Task<ServiceResult> Add(Currency currency)
        {
            var result = await Create(Url, currency);
            _currencies = null;
            return Return(result);
        }

        public async Task<ServiceResult> Edit(Currency currency)
        {
            var result = await Update(Url, currency);
            _currencies = null;
            return Return(result);
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var result = await Delete(Url, id);
            _currencies = null;
            return Return(result);
        }
        public async Task<ServiceResult<Currency>> GetById(int id)
        {
            var result = await _http.GetAsync<Currency>(Url, $"GetById?id={id}");
            return Return<Currency>(result);
        }
    }
}
