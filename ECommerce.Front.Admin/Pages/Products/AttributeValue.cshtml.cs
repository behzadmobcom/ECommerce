using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.Admin.Pages.Products;

public class AttributeValueModel : PageModel
{
    private readonly IProductAttributeGroupService _attributeGroupService;

    public AttributeValueModel(IProductAttributeGroupService attributeGroupService)
    {
        _attributeGroupService = attributeGroupService;
    }

    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public List<ProductAttributeGroup> AttributeGroups { get; set; }
    [BindProperty] public int ProductId { get; set; }

    public async Task OnGet(int id, string search = "", int pageIndex = 1, int quantityPerPage = 10,
        string message = null, string code = null)
    {
        Message = message;
        Code = code;
        ProductId = id;
        var result = await _attributeGroupService.GetByProductId(id);
        if (result.Code == ServiceCode.Success) AttributeGroups = result.ReturnData;
    }

    public async Task<IActionResult> OnPost(List<ProductAttributeGroup> attributeGroups)
    {
        var result = await _attributeGroupService.AddWithAttributeValue(attributeGroups, ProductId);
        return RedirectToAction("/Products/AttributeValue", new {id = ProductId});
    }
}