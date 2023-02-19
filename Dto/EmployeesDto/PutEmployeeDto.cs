using ECommerce.Dto.Base;

namespace ECommerce.Dto.EmployeesDto;

public class PutEmployeeDto : BaseDto
{
    public string? Name { get; set; }

    public string? Title { get; set; }

    public DateTime? HireDateTime { get; set; }

    public int Commission { get; set; }

    public bool IsActive { get; set; }

}
