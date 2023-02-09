using Ecommerce.Entities;
using ECommerce.Dto.BlogsDto;

namespace Dto.BlogCommentsDtos;

public class GetByIdBlogCommentDto : BlogCommentDto 
{
    public bool IsAccepted { get; set; }
    public DateTime DateTime { get; set; } 
    public string? Email { get; set; }

    public int? UserId { get; set; }
    public UserDto? User { get; set; }

    public int? AnswerId { get; set; } 
    public BlogCommentDto? Answer { get; set; }

    public int? BlogId { get; set; }
    public BlogDto? Blog { get; set; }

}
