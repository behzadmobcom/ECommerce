using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.ProductComments;
    public class DeleteModel : PageModel
    {

    private readonly IProductCommentService _productComment;
    private readonly IProductService _productService;

    public DeleteModel(IProductCommentService productComment, IProductService productService)
    {
        _productComment = productComment;
        _productService = productService;
    }

    [BindProperty] public ProductComment ProductComment { get; set; }
    public Product Product { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task OnGet(int id)
    {
        var ProductCommentResult = await _productComment.GetById(id);
        ProductComment = ProductCommentResult.ReturnData;
        int _productId = ProductComment.ProductId ?? default(int);
        var ProductResult = await _productService.GetById(_productId);
        Product = ProductResult.ReturnData;
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            if (ProductComment.AnswerId != null) await _productComment.Delete(ProductComment.AnswerId ?? default(int));
            var result = await _productComment.Delete(ProductComment.Id);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/ProductComments/Index",
                    new { area = "Admin", message = result.Message, code = result.Code.ToString() });
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        return Page();
    }


}
 
