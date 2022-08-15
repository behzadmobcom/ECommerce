using Entities;
using Entities.Helper;
using Entities.ViewModel;
using ECommerce.Services.IServices;

namespace Services.Services;

public class WishListService : IWishListService
{
    private const string Url = "api/WishLists";
    private readonly ICookieService _cookieService;
    private readonly IHttpService _http;

    public WishListService(IHttpService http, ICookieService cookieService)
    {
        _http = http;
        _cookieService = cookieService;
    }

    public async Task<ServiceResult<List<WishListViewModel>>> Load()
    {
        var currentUser = _cookieService.GetCurrentUser();

        if (currentUser.Id != 0)
        {
            var response = await _http.GetAsync<List<WishListViewModel>>(Url, $"GetById?id={currentUser.Id}");

            return new ServiceResult<List<WishListViewModel>>
            {
                Code = ServiceCode.Success,
                Message = "لیست علاقمندی ها",
                ReturnData = response.ReturnData
            };
        }

        return new ServiceResult<List<WishListViewModel>>
        {
            Code = ServiceCode.Info,
            Message = "لطفا ابتدا وارد شوید"
        };
    }

    public async Task<ServiceResult> Add(int productId)
    {
        var currentUser = _cookieService.GetCurrentUser();
        if (currentUser.Id == 0)
            //await _sweet.FireAsync(, "Info");
            return new ServiceResult
            {
                Code = ServiceCode.Info,
                Message = "لطفا ابتدا وارد شوید"
            };
        var wishList = new WishList
        {
            ProductId = productId,
            UserId = currentUser.Id
        };
        var result = await _http.PostAsync(Url, wishList);
        if (result.Code == 0)
            return new ServiceResult
            {
                Code = ServiceCode.Success,
                Message = "علاقمندی شما با موفقیت ذخیره شد"
            };
        return new ServiceResult
        {
            Code = ServiceCode.Error,
            Message = "افزودن علاقمندی با مشکل مواجه شد"
        };
    }

    public async Task<ServiceResult> Delete(int wishListId)
    {
        var deleteResult = await _http.DeleteAsync(Url, wishListId);
        if (deleteResult.Code == 0)
            return new ServiceResult
            {
                Code = ServiceCode.Success,
                Message = "با موفقیت حذف شد"
            };
        //await _sweet.FireAsync("حذف", "با موفقیت حذف شد", "success");

        return new ServiceResult
        {
            Code = ServiceCode.Error,
            Message = "در حذف مشکل پیش آمد"
        };
    }
}