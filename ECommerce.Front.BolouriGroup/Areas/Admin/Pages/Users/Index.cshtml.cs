using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.Users;

public class IndexModel : PageModel
{
    private readonly IUserService _userService;
    public IndexModel(IUserService userService)
    {
        _userService = userService;
    }

    public ServiceResult<List<UserListViewModel>> Users { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }
    [BindProperty] public bool? IsCollegue { get; set; } = null;
    [BindProperty] public bool? IsActive { get; set; } = null;

    public async Task<IActionResult> OnGet(string search = "", int pageNumber = 1, int pageSize = 10,
    string message = null, string code = null, bool? iscollegue=null, bool? isactive=null)
    {
        Message = message;
        Code = code;
        IsCollegue = iscollegue;
        IsActive = isactive;
        var result = await _userService.UserList(search, pageNumber, pageSize, isActive:isactive, isColleague:iscollegue);
        if (result.Code == ServiceCode.Success)
        {
            result.PaginationDetails.Address = "/Users/Index";
            Message = result.Message;
            Code = result.Code.ToString();
            Users = result;
            return Page();
        }

        return RedirectToPage("/index", new { message = result.Message, code = result.Code.ToString() });
    }
}
