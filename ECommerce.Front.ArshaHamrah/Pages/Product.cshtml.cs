using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.ArshaHamrah.Pages;

public class ProductModel : PageModel
{
   private readonly IProductAttributeGroupService _productAttributeGroupService;
    private readonly IProductService _productService;
    private readonly IStarService _starService;
    private readonly IUserService _userService;


    public ProductModel(IProductService productService, IStarService starService, ICartService cartService,
       IProductAttributeGroupService productAttributeGroupService, IUserService userService)
    {
        _productService = productService;
        _starService = starService;
        _productAttributeGroupService = productAttributeGroupService;
        _userService = userService;
    }

    public ProductViewModel Product { get; set; }
    public List<ProductIndexPageViewModel> RelatedProduct { get; set; }
    public double Stars { get; set; }
    public List<ProductAttributeGroup> AttributeGroups { get; set; }

    public async Task OnGet(string productUrl)
    {
        var user = await _userService.GetUser();
        var resultProduct = await _productService.GetProduct(productUrl, user.ReturnData.Id);
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
 
}