using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;

namespace ECommerce.Services.Services;

public class EmployeeService : EntityService<Employee>, IEmployeeService
{
    private const string Url = "api/Employees";
    private readonly IHttpService _http;

    public EmployeeService(IHttpService http) : base(http)
    {
        _http = http;
    }

    public async Task<ServiceResult<List<Employee>>> Load(string search = "", int pageNumber = 0, int pageSize = 10)
    {
        var result = await ReadList(Url, "GetAll");
        return Return(result);
    }

    public async Task<ServiceResult<List<Employee>>> GetAll(string search = "", int pageNumber = 0, int pageSize = 10)
    {
        var result = await ReadList(Url,
            $"Get?PageNumber={pageNumber}&Search={search}&PageSize={pageSize}");
        return Return(result);
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


    public async Task<ServiceResult> Add(Employee employee)
    {
        var result = await Create(Url, employee);
        return Return(result);
    }

    public async Task<ServiceResult> Edit(Employee employee)
    {
        var result = await Update(Url, employee);
        return Return(result);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        var result = await Delete(Url, id);
        return Return(result);
    }

    public async Task<ServiceResult<Employee>> GetById(int id)
    {
        var result = await _http.GetAsync<Employee>(Url, $"GetById?id={id}");
        return Return(result);
    }
}