using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.ProductAttributeGroups;

public class CreateModel : PageModel
{
    private readonly IProductAttributeGroupService _productAttributeGroupService;

    public CreateModel(IProductAttributeGroupService productAttributeGroupService)
    {
        _productAttributeGroupService = productAttributeGroupService;
    }

    [BindProperty] public ProductAttributeGroup ProductAttributeGroup { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var result = await _productAttributeGroupService.Add(ProductAttributeGroup);
            if (result.Code == 0)
                return RedirectToPage("/ProductAttributeGroups/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        return Page();
    }
}