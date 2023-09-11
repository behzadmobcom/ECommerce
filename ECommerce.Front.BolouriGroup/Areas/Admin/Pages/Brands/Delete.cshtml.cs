using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.Brands;

public class DeleteModel : PageModel
{
    private readonly IBrandService _brandService;

    public DeleteModel(IBrandService brandService)
    {
        _brandService = brandService;
    }

    public Brand Brand { get; set; }
    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _brandService.GetById(id);
        if (result.Code == 0)
        {
            Brand = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/Brands/Index",
            new { area = "Admin", message = result.Message, code = result.Code.ToString() });
    }

    public async Task<IActionResult> OnPost(int id)
    {
        if (ModelState.IsValid)
        {
            var result = await _brandService.Delete(id);
            return RedirectToPage("/Brands/Index",
                new { area = "Admin", message = result.Message, code = result.Code.ToString() });
        }

        return Page();
    }
}