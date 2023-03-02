using ECommerce.Dto.Base;

namespace ECommerce.Dto.WishListsDto;

public class WishListDto : BaseDto
{
    public int UserId { get; set; }

    public UserDto? User { get; set; }

    public int ProductId { get; set; }

    public ProductDto? Product { get; set; }
}
