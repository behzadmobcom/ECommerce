using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Entities.ViewModel;

public class LoginViewModel
{
    public int Id { get; set; }

    [Display(Name = "نام کاربری")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    [StringLength(30, MinimumLength = 4, ErrorMessage = @"حداقل 4 و حداکثر 30 کاراکتر")]
    public string Username { get; set; }

    public string? FullName { get; set; }

    [Display(Name = "کلمه عبور")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    [StringLength(30, MinimumLength = 8, ErrorMessage = @"حداقل 8 و حداکثر 30 کاراکتر")]
    public string Password { get; set; }

    public string? Token { get; set; }
    public bool IsActive { get; set; }

    public bool IsColleague { get; set; }

    [Display(Name = "مرا به خاطر بسپار")] public bool RememberMe { get; set; }

    public string? AuthName { get; set; }

    public static implicit operator LoginViewModel(User user)
    {
        return new LoginViewModel
        {
            Username = user.UserName,
            IsColleague = user.IsColleague,
            AuthName = user.UserRole.Name,
            Id = user.Id,
            IsActive = user.IsActive
        };
    }
}