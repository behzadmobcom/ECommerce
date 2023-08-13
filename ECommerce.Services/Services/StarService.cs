using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;

namespace ECommerce.Services.Services;

public class StarService : EntityService<ProductUserRank>, IStarService
{
    private const string Url = "api/Stars";
    private readonly ICookieService _cookieService;
    private readonly IHttpService _http;

    public StarService(IHttpService http, ICookieService cookieService) : base(http)
    {
        _http = http;
        _cookieService = cookieService;
    }

    public int FillStars { get; set; }
    public int EmptyStars { get; set; }
    public int StarCount { get; set; }

    public async Task<ApiResult<List<ProductUserRank>>> Load()
    {
        var currentUser = _cookieService.GetCurrentUser();
        var temp = await _http.GetAsync<List<ProductUserRank>>(Url, $"GetByUserId?id={currentUser.Id}");
        return temp;
    }

    public async Task<ServiceResult> SaveStars(int productId, int starNumber)
    {
        var currentUser = _cookieService.GetCurrentUser();
        if (currentUser.Id == 0) return new ServiceResult
        {
            Message = "برای امتیاز دادن ابتدا لاگین کنید",
            Code = ServiceCode.Error
        };
        var productUserRank = new ProductUserRank
        {
            ProductId = productId,
            UserId = currentUser.Id,
            Stars = starNumber
        };
        var result = await Create(Url, productUserRank);
        return new ServiceResult
        {
            Message = result.Code == 0
            ? "امتیاز شما با موفقیت ذخیره شد"
            : "متاسفانه امتیاز شما ذخیره نشده. به پشتیبانی سایت اطلاع دهید",
            Code = result.Code == 0 ? ServiceCode.Success : ServiceCode.Error
        };
    }

    public async Task<int> SumStarsByProductId(int productId)
    {
        FillStars = 0;
        EmptyStars = 5;
        StarCount = 0;
        var sum = 5;
        var result = await _http.GetAsync<int>(Url, $"GetBySumProductId?id={productId}");
        if (result.Code == 0) sum = result.ReturnData;

        return sum;
    }
}