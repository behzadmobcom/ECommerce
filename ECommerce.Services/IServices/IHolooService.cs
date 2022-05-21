namespace ECommerce.Services.IServices;

public interface IHolooService
{
    Task<string> ConvertHoloo(bool isAllMGroupConvert, string mGroupCode);
}