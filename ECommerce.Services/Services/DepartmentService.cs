using Services.IServices;
using Entities;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Helper;

namespace Services.Services
{
    public class DepartmentService : EntityService<Department>, IDepartmentService
    {

        private const string Url = "api/Departments";
      private List<Department> _departments;
      private readonly IHttpService _http;

        public DepartmentService(IHttpService http) : base(http)
        {
            _http = http;
        }

        public async Task<ServiceResult<List<Department>>> Load(int pageNumber = 0, int pageSize = 10)
        {
            var result = await ReadList(Url, $"Get?PageNumber={pageNumber}&PageSize={pageSize}");
            return Return<List<Department>>(result);
        }
        public async Task<ServiceResult<List<Department>>> Filtering(string filter)
        {
            if (_departments == null)
            {
                var departments = await Load();
                if (departments.Code > 0) return departments;
                _departments = departments.ReturnData;
            }

            var result = _departments.Where(x => x.Title.Contains(filter)).ToList();
            if (result.Count == 0)
            {
                return new ServiceResult<List<Department>> { Code = ServiceCode.Info, Message = "دپارتمان یافت نشد" };
            }
            return new ServiceResult<List<Department>>
            {
                Code = ServiceCode.Success,
                ReturnData = result
            };
        }

        public async Task<ServiceResult> Add(Department department)
        {
            var result = await Create(Url, department);
            _departments = null;
            return Return(result);
        }

        public async Task<ServiceResult> Edit(Department department)
        {
            var result = await Update(Url, department);
            _departments = null;
            return Return(result);
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var result = await Delete(Url, id);
            _departments = null;
            return Return(result);
        }
        public async Task<ServiceResult<Department>> GetById(int id)
        {
            var result = await _http.GetAsync<Department>(Url, $"GetById?id={id}");
            return Return<Department>(result);
        }
    }
}
