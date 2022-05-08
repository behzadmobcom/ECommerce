

using Services.IServices;
using Entities;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Helper;


namespace Services.Services
{
    public class ColorService : EntityService<Color>, IColorService
    {
        private const string Url = "api/Colors";
        private List<Color> _colors;
        private readonly IHttpService _http;

        public ColorService(IHttpService http) : base(http)
        {
            _http = http;
        }
        public async Task<ServiceResult<List<Color>>> Load(string search = "", int pageNumber = 0, int pageSize = 10)
        {
            var result = await ReadList(Url, $"Get?PageNumber={pageNumber}&PageSize={pageSize}&Search={search}");
            return Return<List<Color>>(result);
        }
        public async Task<ServiceResult<List<Color>>> Filtering(string filter)
        {
            if (_colors == null)
            {
                var colors = await Load();
                if (colors.Code > 0) return colors;
                _colors = colors.ReturnData;
            }

            var result = _colors.Where(x => x.Name.Contains(filter)).ToList();
            if (result.Count == 0)
            {
                return new ServiceResult<List<Color>> { Code = ServiceCode.Info, Message = "رنگ یافت نشد" };
            }
            return new ServiceResult<List<Color>>
            {
                Code = ServiceCode.Success,
                ReturnData = result
            };
        }

        public async Task<ServiceResult> Add(Color color)
        {
            var result = await Create(Url, color);
            _colors = null;
            return Return(result);
        }

        public async Task<ServiceResult> Edit(Color color)
        {
            var result = await Update(Url, color);
            _colors = null;
            return Return(result);
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var result = await Delete(Url, id);
            _colors = null;
            return Return(result);
        }
        public async Task<ServiceResult<Color>> GetById(int id)
        {
            var result = await _http.GetAsync<Color>(Url, $"GetById?id={id}");
            return Return<Color>(result);
        }
    }
}
