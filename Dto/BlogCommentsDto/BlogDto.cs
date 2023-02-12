using ECommerce.Dto.Base;

namespace ECommerce.Dto.BlogCommentsDto;

public class BlogDto : BaseDto
{
    public string? Title { get; set; }
    public ImageDto? Image { get; set; }

}
