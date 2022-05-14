using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.ProductAttributes;

public class CreateModel : PageModel
{
    private readonly IProductAttributeService _productAttributeService;

    public CreateModel(IProductAttributeService productAttributeService)
    {
        _productAttributeService = productAttributeService;
    }

    [BindProperty] public ProductAttribute ProductAttribute { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var result = await _productAttributeService.Add(ProductAttribute);
            if (result.Code == 0)
                return RedirectToPage("/ProductAttributes/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        return Page();
    }
}