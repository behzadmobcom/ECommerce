using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Entities;

public class PaymentMethod : BaseEntity
{
    [StringLength(26, MinimumLength = 8, ErrorMessage = "حداقل 8 و حداکثر 26 کاراکتر")]
    [Display(Name = "شماره حساب")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا برای {0} فقط عدد وارد کنید")]
    public string AccountNumber { get; set; }

    [Display(Name = "نام بانک")]
    [StringLength(50)]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    [RegularExpression("^[\\u0600-\\u06FF\\s]+$$", ErrorMessage = "لطفا برای {0} فقط حروف فارسی وارد کنید")]
    public string BankName { get; set; }

    [Display(Name = "نام شعبه")]
    [StringLength(50)]
    [RegularExpression("^[\\u0600-\\u06FF\\s\\d]+$$", ErrorMessage = "لطفا برای {0} فقط حروف فارسی و عدد وارد کنید")]

    public string? BrunchName { get; set; }

    [Display(Name = "کد بانک")]
    [StringLength(2)]
    [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا برای {0} فقط عدد وارد کنید")]

    public string? BankCode { get; set; }

    public PaymentMethodStatus PaymentMethodStatus { get; set; }

    //ForeignKey
    public ICollection<Transaction>? Transactions { get; set; }
}

public enum PaymentMethodStatus
{
    آنلاین = 0,
    واریز = 1,
    pos = 2
}