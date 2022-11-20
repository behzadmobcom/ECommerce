using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Front.BolouriGroup.ViewComponents;

public class BlogCategoriesMenuViewComponent : ViewComponent
{
    private readonly IBlogCategoryService _categoryService;

    public BlogCategoriesMenuViewComponent(IBlogCategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var result = (await _categoryService.GetParents()).ReturnData;
        return View(result);
    }
}