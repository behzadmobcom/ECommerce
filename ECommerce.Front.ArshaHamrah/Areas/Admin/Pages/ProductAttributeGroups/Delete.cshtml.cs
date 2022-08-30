using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ArshaHamrah.Areas.Admin.Pages.ProductAttributeGroups;

[Authorize(Roles = "Admin,SuperAdmin")]
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
            new {area = "Admin", message = result.Message, code = result.Code.ToString()});
    }

    public async Task<IActionResult> OnPost(int id)
    {
        if (ModelState.IsValid)
        {
            var result = await _productAttributeGroupService.Delete(id);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/ProductAttributeGroups/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
        }

        return Page();
    }
}