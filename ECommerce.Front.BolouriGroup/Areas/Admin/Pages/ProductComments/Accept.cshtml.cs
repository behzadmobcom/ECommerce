using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.ProductComments;

public class AcceptModel : PageModel
{
    private readonly IProductCommentService _productComment;
    private readonly IProductService _productService;
    public AcceptModel(IProductCommentService productCommentService, IProductService productService)
    {
        _productComment = productCommentService;
        _productService = productService;
    }

    [BindProperty] public ProductComment ProductComment { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task OnGet(int id, string message = null, string code = null)
    {
        Message = message;
        Code = code;
        var ProductCommentResult = await _productComment.GetById(id);
        ProductComment = ProductCommentResult.ReturnData;
    }
}