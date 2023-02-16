using ECommerce.Dto.Base;

namespace ECommerce.Dto.CategoriesDto;

public class CategoryDto : BaseDto
{
    public string? Name { get; set; }

    public int Depth { get; set; }

    public string? Path { get; set; }

    public bool IsActive { get; set; }

    public int DisplayOrder { get; set; }

    public int? ParentId { get; set; }
    public CategoryDto? Parent { get; set; }

}
