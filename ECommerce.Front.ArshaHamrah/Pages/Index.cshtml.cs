using Ecommerce.Entities;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.ArshaHamrah.Pages;

public class IndexModel : PageModel
{
    private readonly ICartService _cartService;
    private readonly ICompareService _compareService;
    private readonly ICookieService _cookieService;
    private readonly IProductService _productService;
    private readonly ISlideShowService _slideShowService;
    private readonly IWishListService _wishListService;
    private readonly IDiscountService _discountService;
    private readonly IStarService _starService;

    public IndexModel(ISlideShowService slideShowService, IProductService productService,
        IWishListService wishListService, ICartService cartService, ICompareService compareService,
        ICookieService cookieService, IDiscountService discountService, IStarService starService)
    {
        _slideShowService = slideShowService;
        _productService = productService;
        _wishListService = wishListService;
        _cartService = cartService;
        _compareService = compareService;
        _cookieService = cookieService;
        _discountService = discountService;
        _starService = starService;
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
        SlideShowViewModels = (await _slideShowService.TopSlideShow(5)).ReturnData;
        NewProducts = (await _productService.TopNew()).ReturnData;
        NewTop8Products = NewProducts.Take(8).ToList();
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
        var result = await _productService.GetByIdViewModel(id);
        return new JsonResult(result);
    }

    public async Task<JsonResult> OnGetLoadCart(int id)
    {
        var result = await _cartService.Load(HttpContext);
        if (result.Code == 0)
        {
            foreach (var product in result.ReturnData)
            {
                decimal? discount = 0;
                if (product.Discount != null)
                {
                    if (product.Discount.Amount > 0)
                    {
                        discount = product.Discount.Amount;
                    }
                    else if (product.Discount.Percent > 0)
                    {
                        discount = product.PriceAmount * (decimal)product.Discount.Percent / 100;
                    }
                }
                var priceAmount = product.PriceAmount - discount;
                decimal sumPrice = (priceAmount * product.Quantity) ?? 0;

                product.DiscountAmount = discount.Value;
                product.PriceAmount = priceAmount.Value;
                product.SumPrice = sumPrice;
            }
        }
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
        return new JsonResult(result);
    }

    public async Task<IActionResult> OnGetRemoveWishList(int id)
    {
        var result = await _wishListService.Delete(id);
        return new JsonResult(result);
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

    public async Task<IActionResult> OnGetSaveStars(int id, int starNumber)
    {
        var result = await _starService.SaveStars(id, starNumber);
        return new JsonResult(result);
    }
}