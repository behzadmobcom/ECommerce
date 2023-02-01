using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.EmployeesDtos
{
    public class EmployeesGetByIdDto
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public DateTime? HireDateTime { get; set; }

        public int Commission { get; set; }

        public bool IsActive { get; set; }

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
}
