using ECommerce.Dto.BlogsDto;

namespace Dto.BlogCommentsDtos;

public class GetBlogCommentDto : BlogCommentDto
{
      
    public bool IsAccepted { get; set; }
    public string? Email { get; set; }

    public BlogDto? Blog { get; set; }

}
