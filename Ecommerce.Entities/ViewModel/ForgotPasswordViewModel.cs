using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModel;

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
    [Required] public string Email { get; set; }

    [Required] public string Token { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "کلمه عبور")]
    public string Password { get; set; }
}