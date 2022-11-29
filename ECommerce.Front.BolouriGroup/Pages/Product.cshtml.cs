using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages;

public class ProductdetailsModel : PageModel
{
    private readonly IProductAttributeGroupService _attributeGroupService;
    private readonly ICartService _cartService;
    private readonly IProductService _productService;
    private readonly IStarService _starService;
    private readonly ITagService _tagService;
    private readonly IUserService _userService;
    private readonly IProductCommentService _productCommandService;
    
    public ProductdetailsModel(IProductService productService, IStarService starService, ICartService cartService,
        ITagService tagService, IProductAttributeGroupService attributeGroupService, IUserService userService
        , IProductCommentService productCommandService)
    {
        _productService = productService;
        _starService = starService;
        _cartService = cartService;
        _tagService = tagService;
        _attributeGroupService = attributeGroupService;
        _userService = userService;
        _productCommandService = productCommandService;
    }

    public ProductViewModel Product { get; set; }
    public List<Tag> tags { get; set; }
    public List<ProductIndexPageViewModel> RelatedProducts { get; set; }
    public double Stars { get; set; }
    public List<ProductAttributeGroup> AttributeGroups { get; set; }
    public ProductComment? ProductComment { get; set; }
    [BindProperty] public string? Message { get; set; }
    public List<ProductComment> ProductComments { get; set; }
 
    private  async Task Initial(string productUrl)
    {
        var resultProduct = await _productService.GetProduct(productUrl);
        if (resultProduct.Code > 0) return;
        Product = resultProduct.ReturnData;
        var result = await _attributeGroupService.GetByProductId(Product.Id);
        if (result.Code == ServiceCode.Success)
            AttributeGroups = result.ReturnData.Where(x =>
                x.Attribute.Any(a =>
                    a.AttributeValue.Any(i =>
                        i.Value != null))).ToList();

        RelatedProducts = (await _productService.TopRelatives(Product.Id)).ReturnData;

        Stars = await _starService.SumStarsByProductId(Product.Id);

        var reultProductComments = await _productCommandService.GetAllAccesptedComments(Product.Id);
        ProductComments = reultProductComments.ReturnData;
    

    }
    public async Task OnGet(string productUrl)
    {
        await Initial(productUrl);
        
    }

    public async Task OnPost(ProductComment productComment , string productUrl)
    {
        var user = await _userService.GetUser();
        if (user != null)
        {
            productComment.UserId = user.ReturnData.Id;
            productComment.Name = user.ReturnData.UserName;
        }        

        var resultProduct = await _productService.GetProduct(productUrl);
        if (resultProduct.Code > 0) return;
        Product = resultProduct.ReturnData;
        productComment.ProductId = Product.Id;
     
        if (ModelState.IsValid)
        {
            var result = await _productCommandService.Add(productComment);            
            Message = result.Message;
        }
        else
        {
            Message = "نظر شما ثبت نگردید.";
        }
        await Initial(productUrl);
    }
  
}