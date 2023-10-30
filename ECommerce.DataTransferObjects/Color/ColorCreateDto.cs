
namespace ECommerce.DataTransferObjects.Color;

public class ColorCreateDto : ColorBaseDto
{
    public int? CreatorUserId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
