﻿namespace ECommerce.Dto.EmployeesDto;

public class PostEmployeeDto
{
    public string? Name { get; set; }

    public string? Title { get; set; }

    public DateTime? HireDateTime { get; set; }

    public int Commission { get; set; }

    public bool IsActive { get; set; }

    public int? DepartmentId { get; set; }
    public DepartmentDto? Department { get; set; }

}
