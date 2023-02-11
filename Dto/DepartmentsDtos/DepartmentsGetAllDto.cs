using Ecommerce.Entities;

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
