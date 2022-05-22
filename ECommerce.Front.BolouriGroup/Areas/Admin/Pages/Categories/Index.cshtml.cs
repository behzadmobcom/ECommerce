using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;

namespace Bolouri.Areas.Admin.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly ICategoryService _categoryService;

    public IndexModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public ServiceResult<List<Category>> Categories { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(string search = "", int pageNumber = 1, int pageSize = 10,
        string message = null, string code = null)
    {
        Message = message;
        Code = code;
        var result = await _categoryService.Load(search, pageNumber, pageSize);
        if (result.Code == ServiceCode.Success)
        {
            Message = result.Message;
            Code = result.Code.ToString();
            Categories = result;
            return Page();
        }

        return RedirectToPage("/index", new {message = result.Message, code = result.Code.ToString()});
    }
}