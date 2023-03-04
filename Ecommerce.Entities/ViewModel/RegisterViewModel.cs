using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Entities.ViewModel;

public class RegisterViewModel
{
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "لطفا فقط انگلیسی!!!")]
    [Display(Name = "نام کاربری")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    [StringLength(30, MinimumLength = 4, ErrorMessage = @"حداقل 4 و حداکثر 30 کاراکتر")]
    public string Username { get; set; }

    [RegularExpression(@"^(?=.*[a-z])(?=.*\d).{8,30}$",
        ErrorMessage = "لطفا فقط انگلیسی! حداقل 8 حداکثر 30 کاراکتر و حتما باید از حروف انگلیسی، عدد و حروف خاص استفاده شده باشد")]
    [Display(Name = "کلمه عبور")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    [StringLength(30, MinimumLength = 8, ErrorMessage = @"حداقل 8 و حداکثر 30 کاراکتر")]
    public string Password { get; set; }

    [Display(Name = "کلمه عبور")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    [StringLength(30, MinimumLength = 8, ErrorMessage = @"حداقل 8 و حداکثر 30 کاراکتر")]
    [Compare("Password", ErrorMessage = @"تکرار کلمه عبور همخوانی ندارد")]
    public string ConfirmPassword { get; set; }

    //[RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "ایمیل وارد شده دارای فرمت صحیح نمی باشد")]
    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = @"حداقل 8 و حداکثر 50 کاراکتر")]
    [EmailAddress]
    public string Email { get; set; }

    public bool IsHaveEmail { get; set; }

    [Display(Name = "قوانین و مقررارت", Description ="قبول قوانین و مقررات سایت")]
    [Required(ErrorMessage = @"لطفا ایتدا {0} را تایید کنید")]
    public bool IsRole { get; set; } = false;

	[Display(Name = "همکار", Description = "در صورتی که همکار هستید باید مدارک مورد نیاز را ارسال کنید تا تایید شود")]
    public bool IsColleague { get; set; } = false;

    [Display(Name = "موبایل")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "{0} حتما باید 11 کاراکتر باشد")]
    public string Mobile { get; set; }

    [Display(Name = "داشتن کد مشتری در فاکتور های قبلی")]
    public bool IsHaveCustomerCode { get; set; } = false;

    [StringLength(12, ErrorMessage = "حداکثر باید 12 کاراکتر باشد")]
    public string? CustomerCodeCustomer { get; set; }

    //============== UserInformation ==============

    [Display(Name = "نام")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = @"حداکثر 30 کاراکتر")]
    public string? FirstName { get; set; }

    [Display(Name = "نام شرکت")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = @"حداکثر 30 کاراکتر")]
    public string? CompanyName { get; set; }

    [Display(Name = "نوع شرکت")]
    public string? CompanyTypeName { get; set; }

    [Display(Name = "آی دی نوع شرکت")]
    public int CompanyType { get; set; }

    [Display(Name = "نام خانوادگی")]
    [StringLength(50, ErrorMessage = @"حداکثر 50 کاراکتر")]
    public string? LastName { get; set; }

    [Display(Name = "خبرنامه")] public bool IsFeeder { get; set; }

    [Display(Name = "عکس")] public string? PicturePath { get; set; }

    public int StateId { get; set; }

    public int CityId { get; set; }

    [Display(Name = "کدملی")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "تعداد ارقام غلط است")]
    public string NationalCode { get; set; }

    public int? ConfirmCode { get; set; }

    //============== UserColleague ==============

    [Display(Name = "تصویر مدرک")] public string? LicensePath { get; set; }

    [Display(Name = "نام شرکت")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "حداقل 3 و حداکثر 50 کاراکتر")]
    public string? Name { get; set; }

    //============== Convertor ==============

    public static implicit operator User(RegisterViewModel x)
    {
        return new User
        {
            UserName = x.Username,
            PasswordHash = x.Password,
            Email = x.Email,
            IsColleague = x.IsColleague,
            Mobile = x.Mobile,
            IsHaveCustomerCode = x.IsHaveCustomerCode,
            CustomerCodeCustomer = x.CustomerCodeCustomer,
            FirstName = x.FirstName,
            LastName = x.LastName,
            IsFeeder = x.IsFeeder,
            PicturePath = x.PicturePath,
            StateId = x.StateId,
            CityId = x.CityId,
            LicensePath = x.LicensePath,
            CompanyName = x.Name,
            NationalCode = x.NationalCode
        };
    }
}