using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;

namespace ECommerce.Services.Services;

public class BlogAuthorService : EntityService<BlogAuthor>, IBlogAuthorService
{
    private const string Url = "api/BlogAuthors";
    private readonly IHttpService _http;
    private List<BlogAuthor> _blogAuthors;

    public BlogAuthorService(IHttpService http) : base(http)
    {
        _http = http;
    }

    public async Task<ServiceResult<List<BlogAuthor>>> Load(string search = "", int pageNumber = 0, int pageSize = 10)
    {
        var result = await ReadList(Url, $"Get?PageNumber={pageNumber}&PageSize={pageSize}&Search={search}");
        return Return(result);
    }

    public async Task<ServiceResult<List<BlogAuthor>>> GetAll()
    {
        var result = await ReadList(Url, "GetAll");
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

    public async Task<ServiceResult<List<BlogAuthor>>> Filtering(string filter)
    {
        if (_blogAuthors == null)
        {
            var blogAuthors = await Load();
            if (blogAuthors.Code > 0) return blogAuthors;
            _blogAuthors = blogAuthors.ReturnData;
        }

        var result = _blogAuthors.Where(x => x.Name.Contains(filter)).ToList();
        if (result.Count == 0)
            return new ServiceResult<List<BlogAuthor>> {Code = ServiceCode.Info, Message = "برندی یافت نشد"};
        return new ServiceResult<List<BlogAuthor>>
        {
            Code = ServiceCode.Success,
            ReturnData = result
        };
    }

    public async Task<ServiceResult> Add(BlogAuthor blogAuthor)
    {
        var result = await Create(Url, blogAuthor);
        _blogAuthors = null;
        return Return(result);
    }

    public async Task<ServiceResult> Edit(BlogAuthor blogAuthor)
    {
        var result = await Update(Url, blogAuthor);
        _blogAuthors = null;
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

    public async Task<ServiceResult<BlogAuthor>> GetById(int id)
    {
        var result = await _http.GetAsync<BlogAuthor>(Url, $"GetById?id={id}");
        return Return(result);
    }
}