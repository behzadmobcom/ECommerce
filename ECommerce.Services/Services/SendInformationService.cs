using Entities;
using Entities.Helper;
using Services.IServices;

namespace Services.Services;

public class SendInformationService : EntityService<SendInformation>, ISendInformationService
{
    private const string Url = "api/SendInformation";
    private List<SendInformation> _sendInformation;

    public SendInformationService(IHttpService http) : base(http)
    {
    }

    public async Task<ServiceResult<List<SendInformation>>> Filtering(string filter, int id)
    {
        if (_sendInformation == null)
        {
            var sendInformation = await Load(id);
            if (sendInformation.Code > 0) return sendInformation;
            _sendInformation = sendInformation.ReturnData;
        }

        var result = _sendInformation.Where(x => x.Address.Contains(filter)).ToList();
        if (result.Count == 0)
            return new ServiceResult<List<SendInformation>> {Code = ServiceCode.Info, Message = "آدرسی یافت نشد"};
        return new ServiceResult<List<SendInformation>>
        {
            Code = ServiceCode.Success,
            ReturnData = result
        };
    }

    public async Task<ServiceResult<List<SendInformation>>> Load(int id)
    {
        var result = await ReadList(Url, $"GetByUserId?id={id}");
        return Return(result);
    }

    public async Task<ServiceResult<SendInformation>> Find(int id)
    {
        var result = await Read(Url, $"GetById?id={id}");
        return Return(result);
    }

    public async Task<ServiceResult> Add(SendInformation sendInformation)
    {
        var result = await Create(Url, sendInformation);
        _sendInformation = null;
        return Return(result);
    }

    public async Task<ServiceResult> Edit(SendInformation sendInformation)
    {
        var result = await Update(Url, sendInformation);
        _sendInformation = null;
        return Return(result);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        var result = await Delete(Url, id);
        _sendInformation = null;
        return Return(result);
    }
}