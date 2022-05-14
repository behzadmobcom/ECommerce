using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModel;

public class MyAccountViewModel
{
    public int Id { get; set; }

    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "لطفا فقط انگلیسی!!!")]
    [Display(Name = "نام کاربری")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    [StringLength(30, MinimumLength = 4, ErrorMessage = @"حداقل 4 و حداکثر 30 کاراکتر")]
    public string Username { get; set; }

    [Display(Name = "همکار", Description = "در صورتی که همکار هستید باید مدارک مورد نیاز را ارسال کنید تا تایید شود")]
    public bool IsColleague { get; set; }

    [Display(Name = "موبایل")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "حتما باید 11 کاراکتر باشد")]
    public string Mobile { get; set; }

    [Display(Name = "داشتن کد مشتری در فاکتور های قبلی")]
    public bool IsHaveCustomerCode { get; set; }

    [StringLength(12, ErrorMessage = "حداکثر باید 12 کاراکتر باشد")]
    public string? CustomerCodeCustomer { get; set; }

    //============== UserInformation ==============

    [Display(Name = "نام")]
    [StringLength(30, ErrorMessage = @"حداکثر 30 کاراکتر")]
    public string? FirstName { get; set; }

    [Display(Name = "نام خانوادگی")]
    [StringLength(50, ErrorMessage = @"حداکثر 50 کاراکتر")]
    public string? LastName { get; set; }

    [Display(Name = "خبرنامه")] public bool IsFeeder { get; set; }

    [Display(Name = "عکس")] public string? PicturePath { get; set; }

    [Display(Name = "تاریخ تولد")] public DateTime Birthday { get; set; }

    public int? StateId { get; set; }

    public int? CityId { get; set; }

    //============== UserColleague ==============

    [Display(Name = "تصویر مدرک")] public string? LicensePath { get; set; }

    [Display(Name = "نام شرکت")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "حداقل 3 و حداکثر 50 کاراکتر")]
    public string? CompanyName { get; set; }

    //============== UserExtraInfo ==============

    [Display(Name = "کدملی")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "تعداد ارقام غلط است")]
    public string? NationalCode { get; set; }

    [Display(Name = "نام پدر")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "حداقل 3 و حداکثر 30 کاراکتر")]
    public string? FatherName { get; set; }

    //============== Convertor ==============

    public static implicit operator MyAccountViewModel(User x)
    {
        var myAccount = new MyAccountViewModel
        {
            Id = x.Id,
            Username = x.UserName,
            IsColleague = x.IsColleague,
            Mobile = x.Mobile,
            IsHaveCustomerCode = x.IsHaveCustomerCode,
            CustomerCodeCustomer = x.CustomerCodeCustomer ?? "",
            FirstName = x.FirstName,
            LastName = x.LastName,
            IsFeeder = x.IsFeeder,
            PicturePath = x.PicturePath,
            StateId = x.StateId,
            CityId = x.CityId,
            Birthday = x.Birthday,
            FatherName = x.FatherName,
            NationalCode = x.NationalCode
        };
        if (x.IsColleague)
        {
            myAccount.CompanyName = x.CompanyName;
            myAccount.LicensePath = x.LicensePath;
        }

        return myAccount;
    }
}