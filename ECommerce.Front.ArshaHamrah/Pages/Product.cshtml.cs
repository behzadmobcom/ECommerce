using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;
using Entities;
using Entities.Helper;

namespace ArshaHamrah.Pages;

public class ProductModel : PageModel
{
   private readonly IProductAttributeGroupService _productAttributeGroupService;
    private readonly IProductService _productService;
    private readonly IStarService _starService;

    public ProductModel(IProductService productService, IStarService starService, ICartService cartService,
       IProductAttributeGroupService productAttributeGroupService)
    {
        _productService = productService;
        _starService = starService;
        _productAttributeGroupService = productAttributeGroupService;
    }

    public ProductViewModel Product { get; set; }
    public List<ProductIndexPageViewModel> RelatedProduct { get; set; }
    public double Stars { get; set; }
    public List<ProductAttributeGroup> AttributeGroups { get; set; }

    public async Task OnGet(string productUrl)
    {
        var resultProduct = await _productService.GetProduct(productUrl);
        if (resultProduct.Code > 0) return;
        Product = resultProduct.ReturnData;
        var result = await _productAttributeGroupService.GetByProductId(Product.Id);
        if (result.Code == ServiceCode.Success)
            AttributeGroups = result.ReturnData.Where(x =>
                x.Attribute.Any(a =>
                    a.AttributeValue.Any(i =>
                        i.Value != null))).ToList();


        RelatedProduct = (await _productService.TopRelatives(Product.Id)).ReturnData;

        Stars = await _starService.SumStarsByProductId(Product.Id);
    }

    public async Task<IActionResult> OnPostSaveStars(int id, int starNumber)
    {
        var result = await _starService.SaveStars(id, starNumber);
        return new JsonResult(result);
    }
  
}