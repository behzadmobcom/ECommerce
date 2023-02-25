using ECommerce.Dto.Base;

namespace ECommerce.Dto.LoginHistoriesDto;

public class LoginHistoryDto : BaseDto
{
    public int UserId { get; set; }
    public string? IpAddress { get; set; }
    public string? Token { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ExpirationDate { get; set; }

}
