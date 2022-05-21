using Entities;
using Entities.Helper;
using ECommerce.Services.IServices;

namespace Services.Services;

public class SendInformationService : EntityService<SendInformation>, ISendInformationService
{
    private const string Url = "api/SendInformation";
    private readonly ICookieService _cookieService;
    public SendInformationService(IHttpService http, ICookieService cookieService) : base(http)
    {
        _cookieService = cookieService;
    }

    public async Task<ServiceResult<List<SendInformation>>> Load()
    {
        var currentUser = _cookieService.GetCurrentUser();
        var result = await ReadList(Url, $"GetByUserId?id={currentUser.Id}");
        return Return(result);
    }

    public async Task<ServiceResult<SendInformation>> Find(int id)
    {
        var result = await Read(Url, $"GetById?id={id}");
        return Return(result);
    }

    public async Task<ServiceResult<SendInformation>> Add(SendInformation sendInformation)
    {
        var currentUser = _cookieService.GetCurrentUser();
        sendInformation.UserId = currentUser.Id;
        var result = await Create<SendInformation>(Url, sendInformation);
        return Return(result);
    }

    public async Task<ServiceResult> Edit(SendInformation sendInformation)
    {
        var result = await Update(Url, sendInformation);
        return Return(result);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        var result = await Delete(Url, id);
        return Return(result);
    }
}