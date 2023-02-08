using Ecommerce.Entities;
using ECommerce.Dto.Base;
using ECommerce.Dto.BlogsDto;

namespace Dto.BlogCommentsDtos;

public class BlogCommentDto : BaseDto
{
    public string? Text { get; set; }

    public string? Name { get; set; }

}
