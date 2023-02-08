using Ecommerce.Entities;
using ECommerce.Dto.BlogsDto;

namespace ECommerce.Dto.BlogCategoriesDto;

internal class BlogCategoryDto_ :  BlogCategoryDto
{

    public int? Depth { get; set; } = 0;

    public string? Description { get; set; }

    public int? ParentId { get; set; }
    public BlogCategoryDto? Parent { get; set; }

    public ICollection<BlogCategoryDto>? BlogCategories { get; set; }

    public ICollection<BlogDto>? Blogs { get; set; }

}
