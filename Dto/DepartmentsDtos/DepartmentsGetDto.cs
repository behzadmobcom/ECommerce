using Ecommerce.Entities;

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
