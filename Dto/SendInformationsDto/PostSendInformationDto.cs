namespace ECommerce.Dto.SendInformationsDto;

public class PostSendInformationDto  
{
    public string? RecipientName { get; set; }

    public string? Mobile { get; set; }

    public string? Address { get; set; }

    public string? PostalCode { get; set; }
     
    public int StateId { get; set; }
    public StateDto? State { get; set; }

    public int CityId { get; set; }
    public CityDto? City { get; set; }
}