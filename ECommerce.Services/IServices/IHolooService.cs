using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IHolooService
    {
        Task<string> ConvertHoloo(bool isAllMGroupConvert, string mGroupCode);
    }
}
