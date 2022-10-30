using Entities;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;
using Services.Services;
using Entities.Helper;

namespace Bolouri.Pages;

public class IndexModel : PageModel
{
    private readonly IBrandService _brandService;
    private readonly ICartService _cartService;
    private readonly ICompareService _compareService;
    private readonly ICookieService _cookieService;
    private readonly IProductService _productService;
    private readonly ISlideShowService _slideShowService;
    private readonly IWishListService _wishListService;
    private readonly IStarService _starService;
    private readonly IBlogService _blogService;

    public IndexModel(ISlideShowService slideShowService, IProductService productService,
        IWishListService wishListService, ICartService cartService, ICompareService compareService,
        ICookieService cookieService, IBrandService brandService, IStarService starService, IBlogService blogService)
    {
        _slideShowService = slideShowService;
        _productService = productService;
        _wishListService = wishListService;
        _cartService = cartService;
        _compareService = compareService;
        _cookieService = cookieService;
        _brandService = brandService;
        _starService = starService;
        _blogService = blogService;
    }

    public List<SlideShowViewModel> SlideShowViewModels { get; set; }
    public List<ProductIndexPageViewModel> NewProducts { get; set; }
    public List<ProductIndexPageViewModel> ExpensiveProducts { get; set; }
    public List<ProductIndexPageViewModel> StarProducts { get; set; }
    public ServiceResult<List<BlogViewModel>> Blogs { get; set; }
    //public List<ProductIndexPageViewModel> SellProducts { get; set; }
    public List<Brand> Brands { get; set; }
    public bool IsColleague { get; set; }
    
    public async Task OnGetAsync()
    {
        SlideShowViewModels = (await _slideShowService.TopSlideShow(5)).ReturnData;
        NewProducts = (await _productService.TopNew(8)).ReturnData;
        ExpensiveProducts = (await _productService.TopPrice(4)).ReturnData;
        StarProducts = (await _productService.TopStars(10)).ReturnData;
        //SellProducts = (await _productService.TopSells()).ReturnData;
        Brands = (await _brandService.Load()).ReturnData;
        Brands.RemoveAt(0);
        Blogs = await _blogService.TopBlogs("", "", 0, 3);
        var result = _cookieService.GetCurrentUser();
        if (result.Id > 0) IsColleague = result.IsColleague;
        IsColleague = false;

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

    public async Task<IActionResult> OnGetLogout()
    {
        await _cookieService.LogOut();
        return RedirectToPage("/index");
    }

    public async Task<JsonResult> OnGetAddCart(int id,int priceId,int count = 1)
    {
        var result = await _cartService.Add(HttpContext, id, priceId, count);
        return new JsonResult(result);
    }

    public async Task<JsonResult> OnGetLoadCart()
    {
        var result = await _cartService.Load(HttpContext);
        return new JsonResult(result);
    }

    public async Task<JsonResult> OnGetDeleteCart(int id,int productId, int priceId)
    {
        var result = await _cartService.Delete(HttpContext, id,id, priceId);
        return new JsonResult(result);
    }
      public async Task<JsonResult> OnGetDecreaseCart(int id,int productId, int priceId)
    {
        var result = await _cartService.Decrease(HttpContext, id,id, priceId);
        return new JsonResult(result);
    }

    public async Task<IActionResult> OnGetAddWishList(int id)
    {
        var result = await _wishListService.Add(id);
        return new JsonResult(result.ToString());
    }

    public async Task<IActionResult> OnGetRemoveWishList(int id)
    {
        var result = await _wishListService.Delete(id);
        return new JsonResult(result);
    }

    public IActionResult OnGetAddCompareList(int id)
    {
        var result = _compareService.Add(HttpContext, id);
        return new JsonResult(result.Message);
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

    public async Task<JsonResult> OnGetQuickView(int id)
    {
        var result = await _productService.GetByIdViewModel(id);
        return new JsonResult(result);
    }
}