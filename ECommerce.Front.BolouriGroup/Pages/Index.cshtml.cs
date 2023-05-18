using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages;

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
    //public List<ProductIndexPageViewModel> NewProducts { get; set; } = new List<ProductIndexPageViewModel>();
    public List<ProductIndexPageViewModel> ExpensiveProducts { get; set; } = new List<ProductIndexPageViewModel>();
    public List<ProductIndexPageViewModel> NewProducts { get; set; } = new List<ProductIndexPageViewModel>();
    public ServiceResult<List<BlogViewModel>> Blogs { get; set; }
    //public List<ProductIndexPageViewModel> SellProducts { get; set; }
    public List<Brand> Brands { get; set; }
    public bool IsColleague { get; set; }
    
    public async Task OnGetAsync()
    {
        //var productTops = (await _productService.GetTops("TopNew:8,TopPrices:4,TopStars:10")).ReturnData;
        var productTops = (await _productService.GetTops("TopPrices:4,TopNew:10")).ReturnData;
        foreach(var top in productTops)
        {
            switch (top.TopCategory)
            {
                //case "TopNew": NewProducts.Add(top); break;
                case "TopPrices": ExpensiveProducts.Add(top); break;
                case "TopNew": NewProducts.Add(top); break;
            }
        }
        SlideShowViewModels = (await _slideShowService.TopSlideShow(5)).ReturnData;
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
        return RedirectToPage();
    }

    public async Task<JsonResult> OnGetAddCart(int id,int priceId,int count = 1)
    {
        var result = await _cartService.Add(HttpContext, id, priceId, count);
        return new JsonResult(result);
    }

    public async Task<JsonResult> OnGetLoadCart()
    {               
        string dQ = "\"";
        CardResultViewModel cardResult = new CardResultViewModel();
        var result = await _cartService.Load(HttpContext);
        foreach (var product in result.ReturnData)
        {
            var item = $"<li class={dQ}cart-item{dQ} id={dQ}CartDrop-{product.Id}{dQ}> " +
                    $"<div class={dQ}cart-media{dQ}> " +
                    $"<a asp-page={dQ}Product{dQ} asp-route-productUrl={dQ}{product.Url}{dQ}><img src={dQ}/{product.ImagePath}{dQ} alt={dQ}{product.Alt}{dQ}></a>" +
                    $"<button class={dQ}cart-delete{dQ} onclick={dQ}DeleteCart({product.Id},{product.ProductId},{product.PriceId}){dQ}><i class={dQ}far fa-times{dQ}></i></button>" +
                    $"</div>" +
                    $"  <div class={dQ}cart-info-group{dQ}>" +
                    $"<div class={dQ}cart-info{dQ}>" +
                    $"  <h5><a asp-page={dQ}Product{dQ} asp-route-productUrl={product.Url}{dQ}>{product.Name}</a></h5>" +
                    $"  <h6>برند : {product.Brand} </h6> " +
                    $"  <h6> رنگ : {product.ColorName}</h6>" +
                    $"<p>{product.PriceAmount.ToString("N0")}</p> </div>" +
                    $"<div class={dQ}cart-action-group{dQ}>" +
                    $"<div class={dQ}product-action{dQ}>" +
                    $"<button class={dQ}action-minus{dQ} onclick={dQ}DecreaseCart({product.Id},{product.ProductId},{product.PriceId}){dQ} title={dQ}مقدار منهای{dQ}><i class={dQ}far fa-minus{dQ}></i></button>" +
                    $"<input class={dQ}action-input{dQ} title={dQ}تعداد{dQ} type={dQ}text{dQ} name={dQ}quantity{dQ} value={dQ}{product.Quantity}{dQ}> " +
                    $"<button class={dQ}action-plus{dQ} onclick={dQ}AddCart({product.ProductId},{product.PriceId}){dQ} title={dQ}مقدار به علاوه{dQ}><i class={dQ}far fa-plus{dQ}></i></button> </div>" +
                    $"<h6>{product.SumPrice.ToString("N0")}</h6><h6>تومان</h6>" +
                    $" <input hidden={dQ}hidden{dQ} value={dQ}{product.SumPrice.ToString("N0")}{dQ} id={dQ}SumPrice-{product.Id}{dQ}/>" +
                    $"</div> </div> </li>";
            cardResult.CartList = cardResult.CartList + item;
            cardResult.AllPrice = cardResult.AllPrice + product.SumPrice;
        }
        cardResult.CartCount = result.ReturnData.Count;
        return new JsonResult(cardResult);
    }

    public async Task<JsonResult> OnGetDeleteCart(int id,int productId, int priceId)
    {
        var result = await _cartService.Delete(HttpContext, id,id, priceId);
        return new JsonResult(result);
    }
      public async Task<JsonResult> OnGetDecreaseCart(int id,int productId, int priceId)
    {
        var result = await _cartService.Decrease(HttpContext, id, productId, priceId);
        return new JsonResult(result);
    }

    public async Task<IActionResult> OnGetAddWishList(int id)
    {
        var result = await _wishListService.Add(id);
        return new JsonResult(result.Message);
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

    public async Task<JsonResult> OnGetQuickView(int id)
    {
        var result = await _productService.GetByIdViewModel(id);
        return new JsonResult(result);
    }
}