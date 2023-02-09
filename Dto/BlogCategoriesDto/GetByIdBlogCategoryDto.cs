using ECommerce.Dto.BlogCategoriesDto;

namespace Dto.BlogCategoriesDtos;

public class GetByIdBlogCategoryDto : BlogCategoryDto 
{

    public int? Depth { get; set; } = 0;
     
}
