using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;

namespace Bolouri.Pages;

public class ProductdetailsModel : PageModel
{
    private readonly IProductAttributeGroupService _attributeGroupService;
    private readonly ICartService _cartService;
    private readonly IProductService _productService;
    private readonly IStarService _starService;
    private readonly ITagService _tagService;

    public ProductdetailsModel(IProductService productService, IStarService starService, ICartService cartService,
        ITagService tagService, IProductAttributeGroupService attributeGroupService)
    {
        _productService = productService;
        _starService = starService;
        _cartService = cartService;
        _tagService = tagService;
        _attributeGroupService = attributeGroupService;
    }

    public ProductViewModel Product { get; set; }
    public List<Tag> tags { get; set; }
    public List<ProductIndexPageViewModel> RelatedProducts { get; set; }
    public double Stars { get; set; }
    public List<ProductAttributeGroup> AttributeGroups { get; set; }

    public async Task OnGet(string productUrl)
    {
        var resultProduct = await _productService.GetProduct(productUrl);
        if (resultProduct.Code == 0) Product = resultProduct.ReturnData;

        var result = await _attributeGroupService.GetByProductId(Product.Id);
        if (result.Code == ServiceCode.Success)
            AttributeGroups = result.ReturnData.Where(x =>
                x.Attribute.Any(a =>
                    a.AttributeValue.Any(i =>
                        i.Value != null))).ToList();

        RelatedProducts = (await _productService.TopRelatives(Product.Id)).ReturnData;

        Stars = await _starService.SumStarsByProductId(Product.Id);
    }
  
}