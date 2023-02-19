using ECommerce.Dto.Base;

namespace ECommerce.Dto.EmployeesDto;

public class EmployeeDto_ : BaseDto
{
    public string? Name { get; set; }

    public string? Title { get; set; }

    public DateTime? HireDateTime { get; set; }

    public int Commission { get; set; }

    public bool IsActive { get; set; }

    //ForeignKey
    public string? CustomerCode { get; set; }

    public string? CustomerCodeCustomer { get; set; }

    //public ICollection<PurchaseOrder>? PurchaseOrderApprovedBy { get; set; }

    //public ICollection<PurchaseOrder>? PurchaseOrderSubmittedBy { get; set; }

    //public ICollection<PurchaseOrder>? PurchaseOrderAccountant { get; set; }

    //public int? DepartmentId { get; set; }
    //public Department? Department { get; set; }

    //public int? UserRoleId { get; set; }
    //public UserRole? UserRole { get; set; }

    //public ICollection<BlogComment>? BlogComments { get; set; }

    //public int? HolooCompanyId { get; set; } = 1;
    //public HolooCompany? HolooCompany { get; set; }
}
