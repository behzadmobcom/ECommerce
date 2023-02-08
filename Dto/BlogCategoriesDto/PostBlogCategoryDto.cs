using ECommerce.Dto.BlogCategoriesDto;

namespace Dto.BlogCategoriesDtos;

public class PostBlogCategoryDto : BlogCategoryDto
{

    public int? Depth { get; set; } = 0;

    public string? Description { get; set; }

     
}
