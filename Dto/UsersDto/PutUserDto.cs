using ECommerce.Dto.Base;

namespace ECommerce.Dto.UsersDto;
public class PutUserDto : BaseDto
{ 
    public string? Mobile { get; set; }  

    public string? Email { get; set; } 

    public string? CompanyName { get; set; } 

    public string? NationalCode { get; set; } 

    public string? FirstName { get; set; } 

    public string? LastName { get; set; } 
     
    public int? StateId { get; set; }
    public StateDto? State { get; set; }

    public int? CityId { get; set; }
    public CityDto? City { get; set; }
     
}
