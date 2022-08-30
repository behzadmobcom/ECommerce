using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ArshaHamrah.Areas.Admin.Pages.ProductAttributeGroups;

[Authorize(Roles = "Admin,SuperAdmin")]
public class DetailModel : PageModel
{
    private readonly IProductAttributeGroupService _productAttributeGroupService;

    public DetailModel(IProductAttributeGroupService productAttributeGroupService)
    {
        _productAttributeGroupService = productAttributeGroupService;
    }

    public ProductAttributeGroup ProductAttributeGroup { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _productAttributeGroupService.GetById(id);
        if (result.Code == 0)
        {
            ProductAttributeGroup = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/ProductAttributeGroups/Index",
            new {area = "Admin", message = result.Message, code = result.Code.ToString()});
    }
}