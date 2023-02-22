using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Front.BolouriGroup.ViewComponents
{
    public class CategoriesMobileViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoriesMobileViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = (await _categoryService.GetParents()).ReturnData;
            return View(result);
        }
    }
}
