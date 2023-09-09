using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages;

public class ContactModel : PageModel
{
    private readonly IContactService _contactService;
    [BindProperty] public Contact Contact { get; set; }
    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public ContactModel(IContactService contactService)
    {
        _contactService = contactService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var result = await _contactService.Add(Contact);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code != 0)
            {
                return Page();
            }
            ModelState.Clear();
            return Page();
        }

        return Page();
    }
}