using Ecommerce.Entities.Helper;
using Ecommerce.Entities.HolooEntity;
using ECommerce.Services.IServices;

namespace ECommerce.Services.Services;

public class HolooSGroupService : EntityService<HolooSGroup>, IHolooSGroupService
{
    private const string Url = "api/Products";

    public HolooSGroupService(IHttpService http) : base(http)
    {
    }

    public async Task<ServiceResult<List<HolooSGroup>>> Load(string mGroupCode)
    {
        var result = await ReadList(Url, $"GetSGroupByMGroupCode?mCode={mGroupCode}");
        if (result.Code == ResultCode.Success)
            return new ServiceResult<List<HolooSGroup>>
            {
                Code = ServiceCode.Success,
                ReturnData = result.ReturnData,
                Message = result.Messages?.FirstOrDefault()
            };
        return new ServiceResult<List<HolooSGroup>>
        {
            Code = ServiceCode.Error,
            Message = result.GetBody()
        };
    }
}