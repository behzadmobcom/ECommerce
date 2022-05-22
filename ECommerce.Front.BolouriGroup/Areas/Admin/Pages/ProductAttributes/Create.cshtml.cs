using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ECommerce.Services.IServices;

namespace Bolouri.Areas.Admin.Pages.ProductAttributes;

public class CreateModel : PageModel
{
    private readonly IProductAttributeGroupService _productAttributeGroupService;
    private readonly IProductAttributeService _productAttributeService;

    public CreateModel(IProductAttributeService productAttributeService,
        IProductAttributeGroupService productAttributeGroupService)
    {
        _productAttributeService = productAttributeService;
        _productAttributeGroupService = productAttributeGroupService;
    }

    [BindProperty] public ProductAttribute ProductAttribute { get; set; }
    public SelectList AttributeGroup { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task OnGet()
    {
        var attributeGroups = (await _productAttributeGroupService.GetAll()).ReturnData;
        AttributeGroup = new SelectList(attributeGroups, nameof(ProductAttributeGroup.Id),
            nameof(ProductAttributeGroup.Name));
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