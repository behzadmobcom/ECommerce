namespace ECommerce.Dto.BlogCategoriesDto;

public class GetParentsBlogCategoryDto : BlogCategoryDto
{
    public int Depth { get; set; }

    public string? Path { get; set; }
    public bool Checked { get; set; }
    public int DisplayOrder { get; set; }

}
