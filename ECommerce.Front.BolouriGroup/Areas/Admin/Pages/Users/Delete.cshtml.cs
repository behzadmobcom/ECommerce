using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.Users;

public class DeleteModel : PageModel
{
    private readonly IUserService _userService;
    public DeleteModel(IUserService userService)
    {
        _userService = userService;
    }
    public User User { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }
    public async Task<IActionResult> OnGet(int id)
    {
        var result = await _userService.GetById(id);
        if (result.Code == 0)
        {
            User = result.ReturnData;
            return Page();
        }

        return RedirectToPage("/Users/Index",
            new { area = "Admin", message = result.Message, code = result.Code.ToString() });
    }

    public async Task<IActionResult> OnPost(int id)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.Delete(id);
            Message = result.Message;
            Code = result.Code.ToString();
            if (result.Code == 0)
                return RedirectToPage("/Users/Index",
                    new { area = "Admin", message = result.Message, code = result.Code.ToString() });
            Message = result.Message;
            Code = result.Code.ToString();
        }

        return RedirectToPage("/Users/Delete",
                  new { id = id });
    }

}

