namespace ECommerce.Dto.BlogCommentsDto;

public class PostBlogCommentDto : BlogCommentDto
{
    public DateTime DateTime { get; set; }
    public string? Email { get; set; }

    public int? AnswerId { get; set; }
    public BlogCommentDto? Answer { get; set; }
     
}
