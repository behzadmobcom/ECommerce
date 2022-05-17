using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace ArshaHamrah.Pages;

public class ProductModel : PageModel
{
    private readonly ICartService _cartService;
    private readonly IProductService _productService;
    private readonly IStarService _starService;

    public ProductModel(IProductService productService, IStarService starService, ICartService cartService)
    {
        _productService = productService;
        _starService = starService;
        _cartService = cartService;
    }

    public ProductViewModel Product { get; set; }
    public List<ProductIndexPageViewModel> RelatedProduct { get; set; }
    public double Stars { get; set; }

    public async Task OnGetAsync(string productUrl)
    {
        var resultProduct = await _productService.GetProduct(productUrl);
        if (resultProduct.Code == 0) Product = resultProduct.ReturnData;

        RelatedProduct = (await _productService.TopRelatives(Product.Id)).ReturnData;

        Stars = await _starService.SumStarsByProductId(Product.Id);
    }

    public async Task<JsonResult> OnGetAddCart(int id, int priceId)
    {
        var result = await _cartService.Add(HttpContext, id,  priceId);
        return new JsonResult(result);
    }

    public async Task<JsonResult> OnGetLoadCart(int id)
    {
        var result = await _cartService.Load(HttpContext);
        return new JsonResult(result);
    }

    public async Task<JsonResult> OnGetDeleteCart(int id, int productId, int priceId)
    {
        var result = await _cartService.Delete(HttpContext, id,  productId, priceId);
        return new JsonResult(result);
    }
}