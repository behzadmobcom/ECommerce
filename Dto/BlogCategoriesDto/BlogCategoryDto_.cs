using ECommerce.Dto.Base;

namespace ECommerce.Dto.BlogCategoriesDto;

internal class BlogCategoryDto_ :  BaseDto
{
    public int? Depth { get; set; } 

    public string? Description { get; set; }

    public int? ParentId { get; set; }
    public BlogCategoryDto? Parent { get; set; }

    public ICollection<BlogCategoryDto>? BlogCategories { get; set; }

    //public ICollection<BlogDto>? Blogs { get; set; }

}
