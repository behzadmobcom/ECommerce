using ECommerce.Dto.Base;

namespace ECommerce.Dto.SlideShowsDto;

public class SlideShowDto : BaseDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ImagePath { get; set; }

    public int? ProductId { get; set; }
    public ProductDto? Product { get; set; }

    public int? CategoryId { get; set; }
    public CategoryDto? Category { get; set; }
}

