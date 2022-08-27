using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;
using Entities.ViewModel;

namespace ArshaHamrah.Areas.Admin.Pages.Categories;

public class EditModel : PageModel
{
    private readonly ICategoryService _categoryService;

    public EditModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [BindProperty] public Category Category { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }
    public List<CategoryParentViewModel> Categories { get; set; }
    public async Task OnGet(int id)
    {
        var result = await _categoryService.GetById(id);
        var resultParent = await _categoryService.GetParents();
        Category = result.ReturnData;
        Categories = resultParent.ReturnData;
        if (Categories.Any(x => x.Id == Category.ParentId))
            Categories.First(x => x.Id == Category.ParentId).Checked = true;
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var result = await _categoryService.Edit(Category);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/Categories/Index",
                    new { area = "Admin", message = result.Message, code = result.Code.ToString() });
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        var resultParent = await _categoryService.GetParents();
        Categories = resultParent.ReturnData;

        return Page();
    }
}