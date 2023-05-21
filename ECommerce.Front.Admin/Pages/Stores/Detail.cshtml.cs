using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.Admin.Pages.Stores;

public class DetailModel : PageModel
{
    private readonly IStoreService _storeService;

    public DetailModel(IStoreService storeService)
    {
        _storeService = storeService;
    }

    public Store Store { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _storeService.GetById(id);
        if (result.Code == 0)
        {
            Store = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/Stores/Index",
            new {message = result.Message, code = result.Code.ToString()});
    }
}