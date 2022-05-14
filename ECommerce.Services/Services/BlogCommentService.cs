using Entities;
using Entities.Helper;
using Services.IServices;

namespace Services.Services;

public class BlogCommentService : EntityService<BlogComment>, IBlogCommentService
{
    private const string Url = "api/BlogComments";
    private readonly IHttpService _http;
    private List<BlogComment> _blogComments;

    public BlogCommentService(IHttpService http) : base(http)
    {
        _http = http;
    }

    public async Task<ServiceResult<List<BlogComment>>> Load(string search = "", int pageNumber = 0, int pageSize = 10)
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
                ReturnData = result.ReturnData.ToDictionary(item => item.Id, item => item.Name),
                Message = result.Messages?.FirstOrDefault()
            };
        return new ServiceResult<Dictionary<int, string>>
        {
            Code = ServiceCode.Error,
            Message = result.GetBody()
        };
    }

    public async Task<ServiceResult<List<BlogComment>>> Filtering(string filter)
    {
        if (_blogComments == null)
        {
            var blogComments = await Load();
            if (blogComments.Code > 0) return blogComments;
            _blogComments = blogComments.ReturnData;
        }

        var result = _blogComments.Where(x => x.Name.Contains(filter)).ToList();
        if (result.Count == 0)
            return new ServiceResult<List<BlogComment>> {Code = ServiceCode.Info, Message = "برندی یافت نشد"};
        return new ServiceResult<List<BlogComment>>
        {
            Code = ServiceCode.Success,
            ReturnData = result
        };
    }

    public async Task<ServiceResult> Add(BlogComment blogComment)
    {
        var result = await Create(Url, blogComment);
        _blogComments = null;
        return Return(result);
    }

    public async Task<ServiceResult> Edit(BlogComment blogComment)
    {
        var result = await Update(Url, blogComment);
        _blogComments = null;
        return Return(result);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        var result = await Delete(Url, id);
        _blogComments = null;
        return Return(result);
    }

    public async Task<ServiceResult<BlogComment>> GetById(int id)
    {
        var result = await _http.GetAsync<BlogComment>(Url, $"GetById?id={id}");
        return Return(result);
    }
}