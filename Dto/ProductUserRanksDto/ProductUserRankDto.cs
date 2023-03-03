using ECommerce.Dto.Base;

namespace ECommerce.Dto.ProductUserRanksDto;

public class ProductUserRankDto : BaseDto
{
    public int Stars { get; set; }

    public int UserId { get; set; }

    public UserDto? User { get; set; }

    public int ProductId { get; set; }

    public ProductDto? Product { get; set; }

}
