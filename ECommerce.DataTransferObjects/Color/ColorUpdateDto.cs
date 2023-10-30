
namespace ECommerce.DataTransferObjects.Color;

public class ColorUpdateDto : ColorBaseDto
{
    public int Id { get; set; }
    public int? EditorUserId { get; set; }
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
}
