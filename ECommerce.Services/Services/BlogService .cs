using Entities;
using Entities.Helper;
using ECommerce.Services.IServices;
using Entities.ViewModel;

namespace Services.Services;

public class BlogService : EntityService<Blog>, IBlogService
{
    private const string Url = "api/Blogs";
    private readonly IHttpService _http;
    private List<Blog> _blogs;

    public BlogService(IHttpService http) : base(http)
    {
        _http = http;
    }

    public async Task<ServiceResult<List<Blog>>> Load(string search = "", int pageNumber = 0, int pageSize = 10)
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
                ReturnData = result.ReturnData.ToDictionary(item => item.Id, item => item.Title),
                Message = result.Messages?.FirstOrDefault()
            };
        return new ServiceResult<Dictionary<int, string>>
        {
            Code = ServiceCode.Error,
            Message = result.GetBody()
        };
    }

    public async Task<ServiceResult<List<Blog>>> Filtering(string filter)
    {
        if (_blogs == null)
        {
            var blogs = await Load();
            if (blogs.Code > 0) return blogs;
            _blogs = blogs.ReturnData;
        }

        var result = _blogs.Where(x => x.Title.Contains(filter)).ToList();
        if (result.Count == 0)
            return new ServiceResult<List<Blog>> {Code = ServiceCode.Info, Message = "برندی یافت نشد"};
        return new ServiceResult<List<Blog>>
        {
            Code = ServiceCode.Success,
            ReturnData = result
        };
    }

    //public async Task<ServiceResult> Add(Blog blog)
    //{
    //    var result = await Create(Url, blog);
    //    _blogs = null;
    //    return Return(result);
    //}

    public async Task<ServiceResult<Blog>> Add(BlogViewModel blogViewModel)
    {
        //if (blogViewModel.BrandId == 0) blogViewModel.BrandId = null;
        var result = await Create<Blog>(Url, blogViewModel);
        return Return(result);
    }

    public async Task<ServiceResult> Edit(Blog blog)
    {
        var result = await Update(Url, blog);
        _blogs = null;
        return Return(result);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        var result = await Delete(Url, id);
        _blogs = null;
        return Return(result);
    }

    public async Task<ServiceResult<Blog>> GetById(int id)
    {
        var result = await _http.GetAsync<Blog>(Url, $"GetById?id={id}");
        return Return(result);
    }
}