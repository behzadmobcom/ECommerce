using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.Stores;

public class DeleteModel : PageModel
{
    private readonly IStoreService _storeService;

    public DeleteModel(IStoreService storeService)
    {
        _storeService = storeService;
    }

    public Store Store { get; set; }
    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _storeService.GetById(id);
        if (result.Code == 0)
        {
            Store = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/Stores/Index",
            new { area = "Admin", message = result.Message, code = result.Code.ToString() });
    }

    public async Task<IActionResult> OnPost(int id)
    {
        if (ModelState.IsValid)
        {
            var result = await _storeService.Delete(id);
            return RedirectToPage("/Stores/Index",
                new { area = "Admin", message = result.Message, code = result.Code.ToString() });
        }
        return Page();
    }
}