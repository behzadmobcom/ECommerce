using ECommerce.Dto.Base;

namespace ECommerce.Dto.BlogCommentsDto;

internal class GetBlogCommentDto_ : BaseDto
{
    public string? Text { get; set; }

    public bool IsAccepted { get; set; }

    public DateTime DateTime { get; set; }

    public bool IsRead { get; set; }

    public bool IsAnswered { get; set; }

    public string? Email { get; set; }

    public string? Name { get; set; }

    public int? UserId { get; set; }
    public UserDto? User { get; set; }

    public int? AnswerId { get; set; }
    public BlogCommentDto? Answer { get; set; }

    public int? BlogId { get; set; }
    public BlogDto? Blog { get; set; }

    //public int? EmployeeId { get; set; }
    //public Employee? Employee { get; set; }
}
