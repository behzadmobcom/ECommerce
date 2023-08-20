using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;

namespace ECommerce.Services.Services;

public class StateService : EntityService<State>, IStateService
{
    private const string Url = "api/States";
    private readonly IHttpService _http;
    private List<State> _states;

    public StateService(IHttpService http) : base(http)
    {
        _http = http;
    }
    public async Task<ServiceResult<List<State>>> GetAll()
    {
        var result = await ReadList(Url, "GetAll");
        return Return(result);
    }
    public async Task<ServiceResult<List<State>>> Load()
    {
        var result = await ReadList(Url);
        return Return(result);
    }
    public async Task<ServiceResult<List<State>>> GetWithPagination(string search = "", int pageNumber = 0, int pageSize = 10)
    {
        var result = await ReadList(Url, $"GetAllWithPagination?PageNumber={pageNumber}&PageSize={pageSize}&Search={search}");
        return Return(result);
    }
    public async Task<ServiceResult<List<State>>> Filtering(string filter, int id)
    {
        if (_states == null)
        {
            var states = await Load();
            if (states.Code > 0) return states;
            _states = states.ReturnData;
        }

        var result = _states.Where(x => x.Name.Contains(filter)).ToList();
        if (result.Count == 0)
            return new ServiceResult<List<State>> {Code = ServiceCode.Info, Message = "استانی یافت نشد"};
        return new ServiceResult<List<State>>
        {
            Code = ServiceCode.Success,
            ReturnData = result
        };
    }

    public async Task<ServiceResult> Add(State state)
    {
        var result = await Create(Url, state);
        _states = null;
        return Return(result);
    }

    public async Task<ServiceResult> Edit(State state)
    {
        var result = await Update(Url, state);
        _states = null;
        return Return(result);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        var result = await Delete(Url, id);
        _states = null;
        return Return(result);
    }
    public async Task<ServiceResult<State>> GetById(int id)
    {
        var result = await _http.GetAsync<State>(Url, $"GetById?id={id}");
        return Return(result);
    }
}