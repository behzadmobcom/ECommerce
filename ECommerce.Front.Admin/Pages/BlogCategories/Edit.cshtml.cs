using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.Admin.Pages.BlogCategories;

public class EditModel : PageModel
{
    private readonly IBlogCategoryService _blogcategoryService;

    public EditModel(IBlogCategoryService blogcategoryService)
    {
        _blogcategoryService = blogcategoryService;
    }

    [BindProperty] public BlogCategory BlogCategory { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task OnGet(int id)
    {
        var result = await _blogcategoryService.GetById(id);
        BlogCategory = result.ReturnData;
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var result = await _blogcategoryService.Edit(BlogCategory);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/BlogCategories/Index",
                    new {message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        return Page();
    }
}