using ECommerce.Dto.BlogsDto;

namespace Dto.BlogCommentsDtos;

public class GetAllAccesptedCommentsBlogCommentDto : BlogCommentDto
{
      
    public bool IsAccepted { get; set; }
    public DateTime DateTime { get; set; }
    public string? Email { get; set; }

    public int? AnswerId { get; set; }
    public BlogCommentDto? Answer { get; set; }

    public BlogDto? Blog { get; set; }

}
