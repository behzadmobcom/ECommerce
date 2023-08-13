using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Http.Extensions;
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
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IWishListService _wishListService;

    public ProductdetailsModel(IProductService productService, IStarService starService, ICartService cartService,
        ITagService tagService, IProductAttributeGroupService attributeGroupService, IUserService userService
        , IProductCommentService productCommandService, IHttpContextAccessor httpContextAccessor, IWishListService wishListService)
    {
        _productService = productService;
        _starService = starService;
        _cartService = cartService;
        _tagService = tagService;
        _attributeGroupService = attributeGroupService;
        _userService = userService;
        _productCommandService = productCommandService;
        _httpContextAccessor = httpContextAccessor;
        _wishListService = wishListService;
    }
    public string siteUrl { get; set; }
    public ProductViewModel Product { get; set; }
    public int? WishListPriceId { get; set; }
    public List<Tag> tags { get; set; }
    public List<ProductIndexPageViewModel> RelatedProducts { get; set; }
    public double Stars { get; set; }
    public List<ProductAttributeGroup> AttributeGroups { get; set; }
    public ProductComment? ProductComment { get; set; }
    [BindProperty] public string? Message { get; set; }
    public ServiceResult<List<ProductComment>> ProductComments { get; set; }

    private async Task Initial(string productUrl, int pageNumber = 1, int pageSize = 10)
    {
        var user = await _userService.GetUser();
        var resultProduct = await _productService.GetProduct(productUrl, user.ReturnData.Id);
        if (resultProduct.Code > 0) return;
        Product = resultProduct.ReturnData;
        WishListPriceId = resultProduct.ReturnData.WishListPriceId;
        var result = await _attributeGroupService.GetByProductId(Product.Id);
        if (result.Code == ServiceCode.Success)
            AttributeGroups = result.ReturnData.Where(x =>
                x.Attribute.Any(a =>
                    a.AttributeValue.Any(i =>
                        i.Value != null))).ToList();

        RelatedProducts = (await _productService.TopRelatives(Product.Id)).ReturnData;

        Stars = await _starService.SumStarsByProductId(Product.Id);

        ProductComments = await _productCommandService.GetAllAccesptedComments(search: System.Convert.ToString(Product.Id), pageNumber, pageSize);

        string[] url = HttpContext.Request.GetDisplayUrl().Split("/");
        siteUrl = string.Format("{0}//{1}", url[0], url[2]);
    }

    public async Task OnGet(string productUrl, int pageNumber = 1, int pageSize = 10)
    {
        await Initial(productUrl, pageNumber, pageSize);
    }

    public async Task OnPost(ProductComment productComment, string productUrl)
    {
        var user = await _userService.GetUser();
        if (user != null)
        {
            productComment.UserId = user.ReturnData.Id;
            productComment.Name = user.ReturnData.UserName;
        }

        var resultProduct = await _productService.GetProduct(productUrl,user.ReturnData.Id);
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