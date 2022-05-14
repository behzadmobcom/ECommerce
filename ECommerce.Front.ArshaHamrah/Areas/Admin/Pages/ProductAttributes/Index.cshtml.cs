using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.ProductAttributes;

public class IndexModel : PageModel
{
    private readonly IProductAttributeService _productAttributeService;

    public IndexModel(IProductAttributeService productAttributeService)
    {
        _productAttributeService = productAttributeService;
    }

    public List<ProductAttribute> ProductAttributes { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task OnGet(string message = null, string code = null)
    {
        Message = message;
        Code = code;
        var result = await _productAttributeService.Load(0, 10);
        ProductAttributes = result.ReturnData;
    }
}