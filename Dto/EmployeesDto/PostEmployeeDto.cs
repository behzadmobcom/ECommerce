using Ecommerce.Entities;

namespace Dto.EmployeesDtos;

public class PostEmployeeDto : EmployeeDto
{

    public DateTime? HireDateTime { get; set; }

    public int Commission { get; set; }

    public bool IsActive { get; set; }

    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }
     
}
