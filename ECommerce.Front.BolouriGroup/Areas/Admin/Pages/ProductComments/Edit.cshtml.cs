using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.ProductComments;
    
    public class EditModel : PageModel
    {
    private readonly IProductCommentService _productComment;
    private readonly IProductService _productService;
    public EditModel(IProductCommentService productCommentService, IProductService productService)
    {
        _productComment = productCommentService;
        _productService = productService;
    }

    [BindProperty] public ProductComment ProductComment { get; set; }
    public Product Product { get; set; }  
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task OnGet(int id, string message = null, string code = null)
    {
        Message=message;
        Code=code;
        var ProductCommentResult = await _productComment.GetById(id);
        ProductComment = ProductCommentResult.ReturnData;
        int _productId= ProductComment.ProductId ??  default(int);
        var ProductResult = await _productService.GetById(_productId);
        Product = ProductResult.ReturnData;
    }

    public async Task<IActionResult> OnPost()
    {       
        try
        {
            if (ProductComment.Answer!.Text == null && ProductComment.AnswerId == null) ProductComment.Answer = null;
            var result = await _productComment.Edit(ProductComment);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/ProductComments/Index",
                    new { area = "Admin", message = result.Message, code = result.Code.ToString() });
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
            return RedirectToPage("/ProductComments/Edit",
                        new { id = ProductComment.Id, area = "Admin", message = $"پیغام خطا:{Message}", code = Code });
        }
        catch (Exception ex)      
        {
            return RedirectToPage("/ProductComments/Edit",
                        new { id = ProductComment.Id, area = "Admin", message = "پیغام خطای غیر منتظره", code = "Error" });
        }              
    }

}

