using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Front.ArshaHamrah.Pages
{
    public class ResetForgotPasswordModel : PageModel
    {
        private readonly IUserService _userService;

        public ResetForgotPasswordModel(IUserService userService)
        {
            _userService = userService;
        }
        
        [BindProperty] public string token { get; set; }
        [BindProperty] public string username { get; set; }
        [TempData] public string Message { get; set; }

        [TempData] public string Code { get; set; }

        public void OnGet()
        {
            
        }
        
        public async Task<IActionResult> OnGetResetForgot(string password, string conpass,string token,string username)
        {
            var resetForgotPasswordViewModel = new ResetForgotPasswordViewModel
            {
                Password = password,
                ConPass = conpass,
                PasswordResetToken = token,
                Username = username
            };
            var result = await _userService.ChangeForgotPassword(resetForgotPasswordViewModel);
            return new JsonResult(result);
        }
        
        
    }
}
