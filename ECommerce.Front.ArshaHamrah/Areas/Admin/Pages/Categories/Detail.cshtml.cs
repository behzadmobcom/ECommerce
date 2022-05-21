using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.Categories;

public class DetailModel : PageModel
{
    private readonly ICategoryService _categoryService;

    public DetailModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public Category Category { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _categoryService.GetById(id);
        if (result.Code == 0)
        {
            Category = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/Categories/Index",
            new {area = "Admin", message = result.Message, code = result.Code.ToString()});
    }
}