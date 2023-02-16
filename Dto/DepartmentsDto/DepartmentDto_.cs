using ECommerce.Dto.Base;

namespace ECommerce.Dto.DepartmentsDto;

public class DepartmentDto_ : BaseDto
{
    public string? Title { get; set; }

    public string? Location { get; set; }

    //public ICollection<Employee>? Employees { get; set; }

}
