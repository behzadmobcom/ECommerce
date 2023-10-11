using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Entities;

public class Employee : RootEntity
{
    [Display(Name = "نام")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 50 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Name { get; set; }

    [Display(Name = "عنوان شغلی")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = @"حداقل 3 و حداکثر 50 کاراکتر")]
    [Required(ErrorMessage = @"{0} را وارد کنید")]
    public string Title { get; set; }

    [Display(Name = "تاریخ استخدام")] public DateTime? HireDateTime { get; set; }

    [Display(Name = "میزان کمیسیون")] public int Commission { get; set; }

    [Display(Name = "شاغل")] public bool IsActive { get; set; }

    //ForeignKey
    [StringLength(5, MinimumLength = 5, ErrorMessage = "حتما باید 5 کاراکتر باشد")]
    public string? CustomerCode { get; set; }

    [StringLength(12)] public string? CustomerCodeCustomer { get; set; }

    public ICollection<PurchaseOrder>? PurchaseOrderApprovedBy { get; set; }

    public ICollection<PurchaseOrder>? PurchaseOrderSubmittedBy { get; set; }

    public ICollection<PurchaseOrder>? PurchaseOrderAccountant { get; set; }

    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }

    public int? UserRoleId { get; set; }
    public UserRole? UserRole { get; set; }

    public ICollection<BlogComment>? BlogComments { get; set; }

    public int? HolooCompanyId { get; set; } = 1;
    public HolooCompany? HolooCompany { get; set; }
}