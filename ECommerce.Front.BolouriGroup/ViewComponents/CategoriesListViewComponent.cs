using Microsoft.AspNetCore.Mvc;
using ECommerce.Services.IServices;

namespace ECommerce.Front.Bolouri.ViewComponents;

public class CategoriesListViewComponent : ViewComponent
{
    private readonly ICategoryService _categoryService;

    public CategoriesListViewComponent(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var result = (await _categoryService.GetParents()).ReturnData;
        return View(result);
    }
}