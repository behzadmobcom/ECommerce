using Microsoft.AspNetCore.Mvc;
using ECommerce.Services.IServices;

namespace ECommerce.Front.Bolouri.ViewComponents;

public class CategoriesMenuViewComponent : ViewComponent
{
    private readonly ICategoryService _categoryService;

    public CategoriesMenuViewComponent(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var result = (await _categoryService.GetParents()).ReturnData;
        return View(result);
    }
}