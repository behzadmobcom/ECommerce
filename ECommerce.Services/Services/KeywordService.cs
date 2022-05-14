using Entities;
using Entities.Helper;
using Services.IServices;

namespace Services.Services;

public class KeywordService : EntityService<Keyword>, IKeywordService
{
    private const string Url = "api/Keywords";
    private readonly IHttpService _http;
    private List<Keyword> _keywords;

    public KeywordService(IHttpService http) : base(http)
    {
        _http = http;
    }

    public async Task<ServiceResult<List<Keyword>>> Load(string search = "", int pageNumber = 0, int pageSize = 10)
    {
        var result = await ReadList(Url, $"Get?PageNumber={pageNumber}&PageSize={pageSize}&Search={search}");
        return Return(result);
    }

    public async Task<ServiceResult<List<Keyword>>> GetAll()
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
                ReturnData = result.ReturnData.ToDictionary(item => item.Id, item => item.KeywordText),
                Message = result.Messages?.FirstOrDefault()
            };
        return new ServiceResult<Dictionary<int, string>>
        {
            Code = ServiceCode.Error,
            Message = result.GetBody()
        };
    }

    public async Task<ServiceResult<List<Keyword>>> Filtering(string filter)
    {
        if (_keywords == null)
        {
            var keywords = await Load();
            if (keywords.Code > 0) return keywords;
            _keywords = keywords.ReturnData;
        }

        var result = _keywords.Where(x => x.KeywordText.Contains(filter)).ToList();
        if (result.Count == 0)
            return new ServiceResult<List<Keyword>> {Code = ServiceCode.Info, Message = "کلمه کلیدی یافت نشد"};
        return new ServiceResult<List<Keyword>>
        {
            Code = ServiceCode.Success,
            ReturnData = result
        };
    }

    public async Task<ServiceResult> Add(Keyword keyword)
    {
        var result = await Create(Url, keyword);
        _keywords = null;
        return Return(result);
    }

    public async Task<ServiceResult> Edit(Keyword keyword)
    {
        var result = await Update(Url, keyword);
        _keywords = null;
        return Return(result);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        var result = await Delete(Url, id);
        _keywords = null;
        return Return(result);
    }

    public async Task<ServiceResult<List<Keyword>>> GetKeywordsByProductId(int productId)
    {
        var result = await ReadList(Url, $"GetKeywordsByProductId?id={productId}");
        return Return(result);
        //_keywordProductId ??= (await _keywordViewModelEntityService.ReadList(Url,$"GetKeywordsWithProductId")).ReturnData;
        //return _keywordProductId.Where(x => x.ProductsId.Any(y => y == productId)).ToList();
    }

    public async Task<ServiceResult<Keyword>> GetById(int id)
    {
        var result = await _http.GetAsync<Keyword>(Url, $"GetById?id={id}");
        return Return(result);
    }
}