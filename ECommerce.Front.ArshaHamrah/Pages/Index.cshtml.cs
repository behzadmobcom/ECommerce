using Entities.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;
using Entities;

namespace ArshaHamrah.Pages;

public class IndexModel : PageModel
{
    private readonly ICartService _cartService;
    private readonly ICompareService _compareService;
    private readonly ICookieService _cookieService;
    private readonly IProductService _productService;
    private readonly ISlideShowService _slideShowService;
    private readonly IWishListService _wishListService;
    private readonly IDiscountService _discountService;

    public IndexModel(ISlideShowService slideShowService, IProductService productService,
        IWishListService wishListService, ICartService cartService, ICompareService compareService,
        ICookieService cookieService, IDiscountService discountService)
    {
        _slideShowService = slideShowService;
        _productService = productService;
        _wishListService = wishListService;
        _cartService = cartService;
        _compareService = compareService;
        _cookieService = cookieService;
        _discountService = discountService;
    }

    public List<SlideShowViewModel> SlideShowViewModels { get; set; }
    public List<ProductIndexPageViewModel> NewProducts { get; set; }
    public List<ProductIndexPageViewModel> ExpensiveProducts { get; set; }
    public List<ProductIndexPageViewModel> StarProducts { get; set; }
    public List<ProductIndexPageViewModel> NewTop8Products { get; set; }
    public List<ProductIndexPageViewModel> SellProducts { get; set; }
    public Discount Discount { get; set; }

    public bool IsColleague { get; set; }

    public async Task OnGetAsync()
    {
        NewTop8Products = (await _productService.TopProducts("","", 0, 8)).ReturnData;
        SlideShowViewModels = (await _slideShowService.TopSlideShow(5)).ReturnData;
        NewProducts = (await _productService.TopNew()).ReturnData;
        ExpensiveProducts = (await _productService.TopProducts("","",0,3,4)).ReturnData;
        //ExpensiveProducts = (await _productService.TopPrice(4)).ReturnData;
        StarProducts = (await _productService.TopStars(8)).ReturnData;
        SellProducts = (await _productService.TopSells(8)).ReturnData;

        var result = _cookieService.GetCurrentUser();
        if (result.Id > 0) IsColleague = result.IsColleague;
        IsColleague = false;

        var resultDiscount = await _discountService.GetLast();
        Discount = resultDiscount.ReturnData;
    }


    public IActionResult OnPost()
    {
        var stream = new MemoryStream();
        Request.Body.CopyTo(stream);
        stream.Position = 0;
        var ret = "";
        using (var reader = new StreamReader(stream))
        {
            var requestBody = reader.ReadToEnd();
            if (requestBody.Length > 0)
                ret = requestBody;
            // do something
        }

        return new JsonResult(ret);
    }

    public async Task<JsonResult> OnGetDeleteCart(int id, int productId, int priceId)
    {
        var result = await _cartService.Delete(HttpContext, id,  productId, priceId);
        return new JsonResult(result);
    }

    public async Task<JsonResult> OnGetAddCart(int id, int priceId, int count)
    {
        var result = await _cartService.Add(HttpContext, id, priceId, count);
        return new JsonResult(result);
    }

    public async Task<JsonResult> OnGetQuickView(int id)
    {
        var result = await _productService.GetById(id);
        return new JsonResult(result);
    }

    public async Task<JsonResult> OnGetLoadCart(int id)
    {
        var result = await _cartService.Load(HttpContext);
        return new JsonResult(result);
    }

    public async Task<IActionResult> OnGetLogout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToPage("/index");
    }

    public async Task<JsonResult> OnGetDecreaseCart(int id, int productId, int priceId)
    {
        var result = await _cartService.Decrease(HttpContext, id, productId, priceId);
        return new JsonResult(result);
    }

    public async Task<IActionResult> OnGetAddWishList(int id)
    {
        var result = await _wishListService.Add(id);
        return new JsonResult(result.ToString());
    }

    public IActionResult OnGetAddCompareList(int id)
    {
        var result = _compareService.Add(HttpContext, id);
        return new JsonResult(result);
    }

    public IActionResult OnGetDeleteCompare(int id)
    {
        var result = _compareService.Remove(HttpContext, id);
        return new JsonResult(result);
    }
}