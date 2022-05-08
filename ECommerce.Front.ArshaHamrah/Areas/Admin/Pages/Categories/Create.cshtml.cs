using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.Categories
{
    public class CreateModel : PageModel
    {
        [TempData] public string Message { get; set; }
        [TempData] public string Code { get; set; }

        [BindProperty] public Category Category { get; set; }
        public List<CategoryParentViewModel> Categories { get; set; }
      
        private readonly ICategoryService _categoryService;

        public CreateModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task OnGet()
        {
            var result =await _categoryService.GetParents();
            Categories = result.ReturnData;
        }

        public async Task<IActionResult> OnPost()
        {
            Category.Depth++;
            if (Category.ParentId == 0)
            {
                Category.ParentId = null;
                Category.Path = Category.Name;
                Category.Depth--;
            }
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Add(Category);
                if (result.Code == 0)
                    return RedirectToPage("/Categories/Index",
                        new {area = "Admin", message = result.Message, code = result.Code.ToString()});
                Message = result.Message;
                Code = result.Code.ToString();
                ModelState.AddModelError("", result.Message);
            }
            var resultParent = await _categoryService.GetParents();
            Categories = resultParent.ReturnData;
            return Page();
        }
    }
}