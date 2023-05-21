using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.Admin.Pages.Products;

public class DeleteModel : PageModel
{
    private readonly IProductService _productService;

    public DeleteModel(IProductService productService)
    {
        _productService = productService;
    }

    [BindProperty] public Product Product { get; set; }
    [BindProperty] public IFormFile Upload { get; set; }
    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _productService.GetById(id);
        if (result.Code == 0)
        {
            Product = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/Products/Index",
            new {message = result.Message, code = result.Code.ToString()});
    }

    public async Task<IActionResult> OnPost(int id)
    {
        var result = await _productService.Delete(id);

        Message = result.Message;
        Code = result.Code.ToString();
        if (result.Code == 0)
            return RedirectToPage("/Products/Index",
                new {message = result.Message, code = result.Code.ToString()});

        Message = result.Message;
        Code = result.Code.ToString();
        var resultProduct = await _productService.GetById(id);
        Product = resultProduct.ReturnData;

        return Page();
    }
}