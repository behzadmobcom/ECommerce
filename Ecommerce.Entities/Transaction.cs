using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Transaction : BaseEntity
{
    [Display(Name = "تاریخ فاکتور")] 
    public DateTime TransactionDate { get; set; }

    [Display(Name = "قابل پرداخت")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public int Amount { get; set; }

    [StringLength(15)]
    [Display(Name = "کد رهگیری")]
    public string? RefId { get; set; }

    [Display(Name = "کد سند هلو")] public int? SanadCode { get; set; }

    //ForeignKey
    public ICollection<PurchaseOrder>? PurchaseOrders { get; set; }
    public ICollection<int>? PurchaseOrdersId { get; set; }

    public int? UserId { get; set; }
    public User? User { get; set; }

    public int? PaymentMethodId { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }

    public int? HolooCompanyId { get; set; } = 1;
    public HolooCompany? HolooCompany { get; set; }
}