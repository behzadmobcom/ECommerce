using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dto.DepartmentsDtos
{
    public class DepartmentsGetDto
    {
        public string Title { get; set; }

        public string? Location { get; set; }

        //ForeignKey
        public ICollection<Employee>? Employees { get; set; }
    }
}
