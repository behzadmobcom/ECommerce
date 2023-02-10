using Ecommerce.Entities;
using ECommerce.Dto.Base;

namespace Dto.DepartmentsDtos;

public class DepartmentDto_ : BaseDto
{
    public string? Title { get; set; }

    public string? Location { get; set; }

    public ICollection<Employee>? Employees { get; set; }

}
