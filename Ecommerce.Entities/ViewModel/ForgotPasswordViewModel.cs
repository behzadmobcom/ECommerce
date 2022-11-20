using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Entities.ViewModel;

public class ForgotPasswordViewModel
{
    public string EmailOrPhoneNumber { get; set; }
}

public class VerifyCodeViewModel
{
    public string EmailOrPhoneNumber { get; set; }
    public string Code { get; set; }
}

public class ResetPasswordViewModel
{
    [Required] public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "کلمه عبور")]
    public string Password { get; set; }
    public string OldPassword { get; set; }
}