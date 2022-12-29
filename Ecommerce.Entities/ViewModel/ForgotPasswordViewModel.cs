using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Entities.ViewModel;

public class ForgotPasswordViewModel
{
    public string EmailOrPhoneNumber { get; set; }
}

public class ResetForgotPasswordViewModel
{
    [Required]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    public string ConPass { get; set; }
    public string PasswordResetToken { get; set; }
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