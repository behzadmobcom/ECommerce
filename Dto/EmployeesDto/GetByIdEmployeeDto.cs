﻿namespace ECommerce.Dto.EmployeesDto;

public class GetByIdEmployeeDto : EmployeeDto
{

    public DateTime? HireDateTime { get; set; }

    public int Commission { get; set; }

    public bool IsActive { get; set; }
     
}