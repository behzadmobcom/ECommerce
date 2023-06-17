using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;

namespace ECommerce.Services.Services;

public class TransactionService : EntityService<Transaction>, ITransactionService
{
    private const string Url = "api/Transactions";
    private readonly IHttpService _http;

    public TransactionService(IHttpService http) : base(http)
    {
        _http = http;
    }

    public async Task<ServiceResult<List<Transaction>>> Load(string search = "", int pageNumber = 0, int pageSize = 10)
    {
        var result = await ReadList(Url, $"Get?PageNumber={pageNumber}&PageSize={pageSize}&Search={search}");
        return Return(result);
    }

    public async Task<ServiceResult<List<Transaction>>> GetAll()
    {
        var result = await ReadList(Url, "GetAll");
        return Return(result);
    }

    public async Task<ServiceResult> Add(Transaction transaction)
    {
        var result = await Create(Url, transaction);
        return Return(result);
    }

    public async Task<ServiceResult> Edit(Transaction transaction)
    {
        var result = await Update(Url, transaction);
        return Return(result);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        var result = await Delete(Url, id);
        return Return(result);
    }

    public async Task<ServiceResult<Transaction>> GetById(int id)
    {
        var result = await _http.GetAsync<Transaction>(Url, $"GetById?id={id}");
        return Return(result);
    }
}