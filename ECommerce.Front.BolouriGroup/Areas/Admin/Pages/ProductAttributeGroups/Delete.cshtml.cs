using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.ProductAttributeGroups;

public class DeleteModel : PageModel
{
    private readonly IProductAttributeGroupService _productAttributeGroupService;

    public DeleteModel(IProductAttributeGroupService productAttributeGroupService)
    {
        _productAttributeGroupService = productAttributeGroupService;
    }

    public ProductAttributeGroup ProductAttributeGroup { get; set; }
    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _productAttributeGroupService.GetById(id);
        if (result.Code == 0)
        {
            ProductAttributeGroup = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/ProductAttributeGroups/Index",
            new { area = "Admin", message = result.Message, code = result.Code.ToString() });
    }

    public async Task<IActionResult> OnPost(int id)
    {
        if (ModelState.IsValid)
        {
            var result = await _productAttributeGroupService.Delete(id);
            return RedirectToPage("/ProductAttributeGroups/Index",
                new { area = "Admin", message = result.Message, code = result.Code.ToString() });
        }
        return Page();
    }
}