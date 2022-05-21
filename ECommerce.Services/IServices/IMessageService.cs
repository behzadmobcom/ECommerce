using Entities;
using Entities.Helper;

namespace ECommerce.Services.IServices;

public interface IMessageService : IEntityService<Message>
{
    Task<ServiceResult<List<Message>>> Filtering(string filter);
    Task<ServiceResult<List<Message>>> Load();
    Task<ServiceResult> Add(Message message);
    Task<ServiceResult> Edit(Message message);
    Task<ServiceResult> Delete(int id);
}