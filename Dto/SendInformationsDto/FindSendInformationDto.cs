namespace ECommerce.Dto.SendInformationsDto;

public class FindSendInformationDto : SendInformationDto
{  
    public string? Mobile { get; set; } 

    public string? PostalCode { get; set; }
     
    public int StateId { get; set; }
    public StateDto? State { get; set; }

    public int CityId { get; set; }
    public CityDto? City { get; set; }
}