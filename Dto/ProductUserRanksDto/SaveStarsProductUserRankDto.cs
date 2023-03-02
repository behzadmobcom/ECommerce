namespace ECommerce.Dto.ProductUserRanksDto;

public class SaveStarsProductUserRankDto  
{
    public int Stars { get; set; }

    public int UserId { get; set; }

    public UserDto? User { get; set; }

    public int ProductId { get; set; }

    public ProductDto? Product { get; set; }

}
