using ECommerce.Dto.Base;

namespace ECommerce.Dto.SendInformationsDto;

public class SendInformationDto : BaseDto
    {
        public string? RecipientName { get; set; } 

        public string? Address { get; set; } 
    }