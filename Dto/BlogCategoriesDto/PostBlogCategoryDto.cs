using ECommerce.Dto.BlogCategoriesDto;

namespace Dto.BlogCategoriesDtos;

public class PostBlogCategoryDto : BlogCategoryDto
{

    public int? Depth { get; set; } 

    public string? Description { get; set; }

     
}
