using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.BlogCategories;

public class DetailModel : PageModel
{
    private readonly IBlogCategoryService _blogcategoryService;

    public DetailModel(IBlogCategoryService blogcategoryService)
    {
        _blogcategoryService = blogcategoryService;
    }

    public BlogCategory BlogCategory { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _blogcategoryService.GetById(id);
        if (result.Code == 0)
        {
            BlogCategory = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/BlogCategories/Index",
            new {area = "Admin", message = result.Message, code = result.Code.ToString()});
    }
}