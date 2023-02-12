namespace ECommerce.Dto.BlogCommentsDto;

public class GetBlogCommentDto : BlogCommentDto
{
      
    public bool IsAccepted { get; set; }

    public BlogDto? Blog { get; set; }

}
