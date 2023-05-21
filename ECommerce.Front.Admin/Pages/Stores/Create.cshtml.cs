using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.Admin.Pages.Stores;

public class CreateModel : PageModel
{
    private readonly IStoreService _storeService;

    public CreateModel(IStoreService storeService)
    {
        _storeService = storeService;
    }

    [BindProperty] public Store Store { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var result = await _storeService.Add(Store);
            if (result.Code == 0)
                return RedirectToPage("/Stores/Index",
                    new {message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        return Page();
    }
}