using ECommerce.Dto.Base;

namespace ECommerce.Dto.ProductsDto;

public class DeleteProductDto : BaseDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Review { get; set; }

    public int MinOrder { get; set; } 

    public int? MaxOrder { get; set; }

    public double? MinInStore { get; set; } 

    public int? ReorderingLevel { get; set; }

    public bool IsDiscontinued { get; set; }

    public bool IsActive { get; set; }

    public string? Url { get; set; }

    public int StoreId { get; set; } 
    public StoreDto? Store { get; set; }

    public int SupplierId { get; set; } 
    public SupplierDto? Supplier { get; set; }

    public int? BrandId { get; set; }
    public BrandDto? Brand { get; set; }

    public ICollection<ImageDto>? Images { get; set; }

}
