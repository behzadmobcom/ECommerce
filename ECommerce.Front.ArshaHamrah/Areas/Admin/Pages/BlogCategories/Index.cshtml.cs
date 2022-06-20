using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.BlogCategories;

public class IndexModel : PageModel
{
    private readonly IBlogCategoryService _blogCategoryService;

    public IndexModel(IBlogCategoryService blogCategoryService)
    {
        _blogCategoryService = blogCategoryService;
    }

    public ServiceResult<List<BlogCategory>> BlogCategories { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(string search = "", int pageNumber = 1, int pageSize = 10,
        string message = null, string code = null)
    {
        Message = message;
        Code = code;
        var result = await _blogCategoryService.GetAll(search, pageNumber, pageSize);
        if (result.Code == ServiceCode.Success)
        {
            Message = result.Message;
            Code = result.Code.ToString();
            BlogCategories = result;
            return Page();
        }

        return RedirectToPage("/index", new {message = result.Message, code = result.Code.ToString()});
    }
}