using ECommerce.Dto.Base;

namespace ECommerce.Dto.ProductSellCountsDto;

public class ProductSellCountDto : BaseDto
{
    public int ProductId { get; set; }

    public int Count { get; set; }

    public ProductDto? Product { get; set; }

}
