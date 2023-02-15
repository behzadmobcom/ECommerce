using ECommerce.Dto.Base;

namespace ECommerce.Dto.CategoriesDto;

public class CategoryDto_ : BaseDto
{
    public string? Name { get; set; }

    public int Depth { get; set; } 

    public string? Path { get; set; }

    public bool IsActive { get; set; } 

    public int DisplayOrder { get; set; } 

    public int? ParentId { get; set; }
    public CategoryDto? Parent { get; set; }

    public ICollection<CategoryDto>? Categories { get; set; } 

    //public ICollection<SlideShow>? SlideShows { get; set; }

    //public ICollection<Product>? Products { get; set; }

    //public int? DiscountId { get; set; }
    //public Discount? Discount { get; set; }

}
