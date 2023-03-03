namespace ECommerce.Dto.ProductCommentsDto;

public class GetByIdProductCommentDto : ProductCommentDto
{
    public bool IsAccepted { get; set; }

    public DateTime DateTime { get; set; }

    public int? UserId { get; set; }
    public UserDto? User { get; set; }

    public int? AnswerId { get; set; }
    public ProductCommentDto? Answer { get; set; }
     
}