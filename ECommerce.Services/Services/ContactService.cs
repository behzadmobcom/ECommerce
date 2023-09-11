using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;

namespace ECommerce.Services.Services;

public class ContactService : EntityService<Contact>, IContactService
{
    private const string Url = "api/Contacts";
    private readonly IHttpService _http;

    public ContactService(IHttpService http) : base(http)
    {
        _http = http;
    }

    public async Task<ServiceResult<List<Contact>>> Load()
    {
        var result = await ReadList(Url, "GetAll");
        return Return(result);
    }

    public async Task<ServiceResult<List<Contact>>> GetAll(string search = "", int pageNumber = 0, int pageSize = 10)
    {
        var result = await ReadList(Url,
            $"GetAllWithPagination?PageNumber={pageNumber}&Search={search}&PageSize={pageSize}");
        return Return(result);
    }

    public async Task<ServiceResult> Add(Contact contact)
    {
        var result = await Create(Url, contact);
        return Return(result);
    }

    public async Task<ServiceResult> Edit(Contact contact)
    {
        var result = await Update(Url, contact);
        return Return(result);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        //var result = await Delete(Url, id);
        //return Return(result);
        var result = await _http.DeleteAsync(Url, id);
        if (result.Code == ResultCode.Success)
        {
            return new ServiceResult
            {
                Code = ServiceCode.Success,
                Message = "با موفقیت حذف شد"
            };
        }
        return new ServiceResult { Code = ServiceCode.Error, Message = "به علت وابستگی با عناصر دیگر امکان حذف وجود ندارد" };
    }

    public async Task<ServiceResult<Contact>> GetById(int id)
    {
        var result = await _http.GetAsync<Contact>(Url, $"GetById?id={id}");
        return Return(result);
    }

    public async Task<ServiceResult<Contact?>> GetByName(string name)
    {
        var result = await _http.GetAsync<Contact>(Url, $"GetByName?name={name}");
        return Return(result);
    }

    public async Task<ServiceResult<Contact?>> GetByEmail(string email)
    {
        var result = await _http.GetAsync<Contact>(Url, $"GetByEmail?email={email}");
        return Return(result);
    }
}