using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ArshaHamrah.Areas.Admin.Pages.Stores;

[Authorize(Roles = "Admin,SuperAdmin")]
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
            new {area = "Admin", message = result.Message, code = result.Code.ToString()});
    }
}