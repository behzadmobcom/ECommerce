using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Entities;

public class PaymentMethod : BaseEntity
{
    [StringLength(50)]
    [Display(Name = "شماره حساب")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string AccountNumber { get; set; }

    [Display(Name = "نام بانک")]
    [StringLength(50)]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string BankName { get; set; }

    [Display(Name = "نام شعبه")]
    [StringLength(50)]
    public string? BrunchName { get; set; }

    [Display(Name = "کد بانک")]
    [StringLength(2)]
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