using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.Categories;

public class DeleteModel : PageModel
{
    private readonly ICategoryService _categoryService;

    public DeleteModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public Category Category { get; set; }
    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

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

    public async Task<IActionResult> OnPost(int id)
    {
        if (ModelState.IsValid)
        {
            var result = await _categoryService.Delete(id);
            return RedirectToPage("/Categories/Index",
                new { area = "Admin", message = result.Message, code = result.Code.ToString() });
        }
        return Page();
    }
}