using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.DepartmentsDtos
{
    public class DepartmentsGetAllDto
    {
        public string Title { get; set; }

        public string? Location { get; set; }

        //ForeignKey
        public ICollection<Employee>? Employees { get; set; }
    }
}
