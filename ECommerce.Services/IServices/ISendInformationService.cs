using Entities;
using Entities.Helper;

namespace ECommerce.Services.IServices;

public interface ISendInformationService
{
    Task<ServiceResult<List<SendInformation>>> Load();
    Task<ServiceResult<SendInformation>> Find(int id);
    Task<ServiceResult<SendInformation>> Add(SendInformation sendInformation);
    Task<ServiceResult> Edit(SendInformation sendInformation);
    Task<ServiceResult> Delete(int id);
}