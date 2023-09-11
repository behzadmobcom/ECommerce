using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.ProductAttributes;

public class DeleteModel : PageModel
{
    private readonly IProductAttributeService _productAttributeService;

    public DeleteModel(IProductAttributeService productAttributeService)
    {
        _productAttributeService = productAttributeService;
    }

    public ProductAttribute ProductAttribute { get; set; }
    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _productAttributeService.GetById(id);
        if (result.Code == 0)
        {
            ProductAttribute = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/ProductAttributes/Index",
            new { area = "Admin", message = result.Message, code = result.Code.ToString() });
    }

    public async Task<IActionResult> OnPost(int id)
    {
        if (ModelState.IsValid)
        {
            var result = await _productAttributeService.Delete(id);
            return RedirectToPage("/ProductAttributes/Index",
                new { area = "Admin", message = result.Message, code = result.Code.ToString() });
        }
        return Page();
    }
}