using Ecommerce.Entities;

namespace Dto.BlogCommentsDtos
{
    public class BlogCommentsGetByIdDto
    {
        
        public string Text { get; set; }

        public bool IsAccepted { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateTime { get; set; }

        public bool IsRead { get; set; }

        public bool IsAnswered { get; set; }

        public string? Email { get; set; }

        public string? Name { get; set; }

        //ForeignKey
        public int? UserId { get; set; }
        public User? User { get; set; }

        public int? AnswerId { get; set; }
        public BlogComment? Answer { get; set; }

        public int? BlogId { get; set; }
        public Blog? Blog { get; set; }

        public int? EmployeeId { get; set; }

        public Employee? Employee { get; set; }
    }
}
