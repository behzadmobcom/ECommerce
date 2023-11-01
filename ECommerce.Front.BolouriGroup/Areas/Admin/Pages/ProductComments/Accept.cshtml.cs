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
    [TempData] public string? Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task OnGet(int id, string message = null, string code = null)
    {
        Message = message;
        Code = code;
        var ProductCommentResult = await _productComment.GetById(id);
        ProductComment = ProductCommentResult.ReturnData;
    }
    public async Task<IActionResult> OnPost()
    {
        try
        {
            if (ProductComment.Answer!.Text == null && ProductComment.AnswerId == null) ProductComment.Answer = null;
            var result = await _productComment.Accept(ProductComment);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/ProductComments/Index",
                    new { area = "Admin", message = result.Message, code = result.Code.ToString() });
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
            return RedirectToPage("/ProductComments/Accept",
                        new { id = ProductComment.Id, area = "Admin", message = $"پیغام خطا:{Message}", code = Code });
        }
        catch 
        {
            return RedirectToPage("/ProductComments/Accept",
                        new { id = ProductComment.Id, area = "Admin", message = "پیغام خطای غیر منتظره", code = "Error" });
        }
    }
}