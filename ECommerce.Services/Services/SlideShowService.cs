using Entities.Helper;
using Entities.ViewModel;
using Services.IServices;

namespace Services.Services;

public class SlideShowService : EntityService<SlideShowViewModel>, ISlideShowService
{
    private const string Url = "api/SlideShows";
    private readonly IHttpService _http;
    private List<SlideShowViewModel> _slideShows;

    public SlideShowService(IHttpService http) : base(http)
    {
        _http = http;
    }

    public async Task<ServiceResult<List<SlideShowViewModel>>> Load(int pageNumber = 0, int pageSize = 10)
    {
        var result = await ReadList(Url, $"Get?PageNumber={pageNumber}&PageSize={pageSize}");
        return Return(result);
    }

    public async Task<ServiceResult<List<SlideShowViewModel>>> TopSlideShow(int top)
    {
        if (_slideShows == null)
        {
            var slideShows = await Load();
            if (slideShows.Code > 0) return slideShows;
            _slideShows = slideShows.ReturnData;
        }


        var result = _slideShows.OrderBy(x => x.DisplayOrder).Take(top).ToList();
        if (result.Count == 0)
            return new ServiceResult<List<SlideShowViewModel>>
                {Code = ServiceCode.Info, Message = "اسلایدشویی یافت نشد"};
        return new ServiceResult<List<SlideShowViewModel>>
        {
            Code = ServiceCode.Success,
            ReturnData = result
        };
    }

    public async Task<ServiceResult<List<SlideShowViewModel>>> Filtering(string filter)
    {
        if (_slideShows == null)
        {
            var slideShows = await Load();
            if (slideShows.Code > 0) return slideShows;
            _slideShows = slideShows.ReturnData;
        }

        var result = _slideShows.Where(x => x.Name.Contains(filter)).ToList();
        if (result.Count == 0)
            return new ServiceResult<List<SlideShowViewModel>>
                {Code = ServiceCode.Info, Message = "اسلایدشویی یافت نشد"};
        return new ServiceResult<List<SlideShowViewModel>>
        {
            Code = ServiceCode.Success,
            ReturnData = result
        };
    }

    public async Task<ServiceResult> Add(SlideShowViewModel slideShowViewModel)
    {
        var result = await Create(Url, slideShowViewModel);
        _slideShows = null;
        return Return(result);
    }

    public async Task<ServiceResult> Edit(SlideShowViewModel slideShowViewModel)
    {
        var result = await Update(Url, slideShowViewModel);
        _slideShows = null;
        return Return(result);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        var result = await Delete(Url, id);
        _slideShows = null;
        return Return(result);
    }

    public async Task<ServiceResult<SlideShowViewModel>> GetById(int id)
    {
        var result = await _http.GetAsync<SlideShowViewModel>(Url, $"GetById?id={id}");
        return Return(result);
    }
}