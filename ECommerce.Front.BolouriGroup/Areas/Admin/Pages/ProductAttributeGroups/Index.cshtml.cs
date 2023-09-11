using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.ProductAttributeGroups;

public class IndexModel : PageModel
{
    private readonly IProductAttributeGroupService _productAttributeGroupService;

    public IndexModel(IProductAttributeGroupService productAttributeGroupService)
    {
        _productAttributeGroupService = productAttributeGroupService;
    }

    public ServiceResult<List<ProductAttributeGroup>> ProductAttributeGroups { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(string search = "", int pageNumber = 1, int pageSize = 10,
        string message = null, string code = null)
    {
        Message = message;
        Code = code;
        var result = await _productAttributeGroupService.Load(search, pageNumber, pageSize);
        if (result.Code == ServiceCode.Success)
        {
            result.PaginationDetails.Address = "/ProductAttributeGroups/Index";
            if (Message != null)
            {
                Message = Message;
                Code = Code;
            }
            else
            {
                Message = result.Message;
                Code = result.Code.ToString();
            };
            ProductAttributeGroups = result;
            return Page();
        }

        return RedirectToPage("/index", new { message = result.Message, code = result.Code.ToString() });
    }
}