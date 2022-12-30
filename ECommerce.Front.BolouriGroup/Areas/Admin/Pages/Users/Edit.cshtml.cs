using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.Users;

public class EditModel : PageModel
{
    private readonly IUserService _userService;
    public EditModel(IUserService userService)
    {
        _userService = userService;
    }

    [BindProperty] public User User { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task OnGet(int id)
    {       
        var result = await _userService.GetById(id);
        User = result.ReturnData;
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.Edit(User);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/Users/Index",
                    new { area = "Admin", message = result.Message, code = result.Code.ToString() });
            Message = result.Message==null ? "پیغام خطای غیرمنتظره" : result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", Message);
        }

        return Page();
    }
}
 
