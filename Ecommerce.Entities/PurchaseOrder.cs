using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities;

public class PurchaseOrder : BaseEntity
{
    public Guid OrderGuid { get; set; } = Guid.NewGuid();

    [Display(Name = "تاریخ خرید")] public DateTime CreationDate { get; set; } = DateTime.Now;

    [Display(Name = "وضعیت سفارش")] public Status? Status { get; set; }

    // تاریخ مورد انتضار برای خرید. این گزینه برای خرید های قسطی پر می گردد. و تاریخ قطعی شدن پرداخت بعنوان تاریخ انتضار خرید درج خواهد گردید.
    [Display(Name = "تاریخ انتظار")] public DateTime? ExpectedDate { get; set; }

    [Display(Name = "قابل پرداخت")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public int Amount { get; set; }

    [Display(Name = "تخفیف")] public int? DiscountAmount { get; set; }

    [Display(Name = "هزینه حمل")] public int? ShippingFee { get; set; }

    [Display(Name = "مالیات")] public int? Taxes { get; set; }

    [Display(Name = "تاریخ پرداخت")] public DateTime? PaymentDate { get; set; }

    [Display(Name = "تاریخ تحویل")] public DateTime? DueDate { get; set; }

    [Display(Name = "توضیحات")] public string? Description { get; set; }

    [Display(Name = "تاریخ تائید")] public DateTime? ApprovedDate { get; set; }

    [Display(Name = "تاریخ ارسال")] public DateTime? SubmittedDate { get; set; }

    [Display(Name = "تاریخ تایید حسابداری")]
    public DateTime? AccountantDate { get; set; }

    [Display(Name = "وضعیت پرداخت")] public bool IsPaid { get; set; } = false;

    [StringLength(6)]
    [Display(Name = "کد فاکتور")]
    public string? FBailCode { get; set; }

    //ForeignKey
    public int? ShippingId { get; set; }
    public Shipping? Shipping { get; set; }

    //ForeignKey
    public int UserId { get; set; }

    [JsonIgnore] public User? User { get; set; }

    public ICollection<PurchaseOrderDetail>? PurchaseOrderDetails { get; set; }

    // نام مدیر فروش تائید کننده را باز میگرداند
    public int? ApprovedById { get; set; }

    [ForeignKey("ApprovedById")]
    [InverseProperty("PurchaseOrderApprovedBy")]
    public Employee? ApprovedBy { get; set; }

    // نام مسئول فروش ارسال کننده خرید را مشخص می کند.
    public int? SubmittedById { get; set; }

    [ForeignKey("SubmittedById")]
    [InverseProperty("PurchaseOrderSubmittedBy")]
    public Employee? SubmittedBy { get; set; }

    // نام حسابدار
    public int? AccountantId { get; set; }

    [ForeignKey("AccountantId")]
    [InverseProperty("PurchaseOrderAccountant")]
    public Employee? Accountant { get; set; }

    public int? DiscountId { get; set; }
    public Discount? Discount { get; set; }

    public int? TransactionId { get; set; }
    public Transaction? Transaction { get; set; }
}

// وضعیت سفارش
public enum Status
{
    New = 0,
    Sent = 1,
    Accepted = 2,
    Closed = 3
}