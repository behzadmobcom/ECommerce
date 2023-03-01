using ECommerce.Dto.Base;

namespace ECommerce.Dto.ProductsDto;

public class ProductDto : BaseDto
{
    public string? Name { get; set; }

    public bool IsShowInIndexPage { get; set; }

    public string? Description { get; set; }

    public string? Review { get; set; }

    public double Star { get; set; }

    public int MinOrder { get; set; } 

    public int? MaxOrder { get; set; }

    public double? MinInStore { get; set; } 

    public int? ReorderingLevel { get; set; }

    public bool IsDiscontinued { get; set; }

    public bool IsActive { get; set; }

    public string? Url { get; set; }

}
