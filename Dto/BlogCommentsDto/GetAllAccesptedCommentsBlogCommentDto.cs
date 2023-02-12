namespace ECommerce.Dto.BlogCommentsDto;

public class GetAllAccesptedCommentsBlogCommentDto : BlogCommentDto
{
      
    public bool IsAccepted { get; set; }
    public DateTime DateTime { get; set; }
    public string? Email { get; set; }

    public int? AnswerId { get; set; }
    public BlogCommentDto? Answer { get; set; }
     

}
