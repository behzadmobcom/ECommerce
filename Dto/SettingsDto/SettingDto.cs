using ECommerce.Dto.Base;

namespace ECommerce.Dto.SettingsDto;

public class SettingDto : BaseDto
{
    public string? Name { get; set; }

    public string? Value { get; set; }

}

