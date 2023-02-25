using ECommerce.Dto.Base;

namespace ECommerce.Dto.KeywordsDtos;

public class MessagesDto : BaseDto
{
    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Body { get; set; }

    public int? UserId { get; set; }
    public DateTime DateTime { get; set; }
    //public virtual User User { get; set; }

}
