using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.Units;

public class IndexModel : PageModel
{
    private readonly IUnitService _unitService;

    public IndexModel(IUnitService unitService)
    {
        _unitService = unitService;
    }

    public ServiceResult<List<Unit>> Units { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(string search = "", int pageNumber = 1, int pageSize = 10,
        string message = null, string code = null)
    {
        Message = message;
        Code = code;
        var result = await _unitService.Load(search, pageNumber, pageSize);
        if (result.Code == ServiceCode.Success)
        {
            result.PaginationDetails.Address = "/Units/Index";
            if (Message != null)
            {
                Message = Message;
                Code = Code;
            }
            else
            {
                Message = result.Message;
                Code = result.Code.ToString();
            }
            Units = result;
            return Page();
        }

        return RedirectToPage("/index", new {message = result.Message, code = result.Code.ToString()});
    }
}