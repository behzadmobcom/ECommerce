using ECommerce.Dto.Base;

namespace ECommerce.Dto.ImagesDtos;

public class GetImageByBlogIdImageDto : BaseDto
{
    public string? Name { get; set; }

    public string? Path { get; set; }

    public string? Alt { get; set; }

}
