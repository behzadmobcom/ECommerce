using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Entities;
using Entities.Helper;

namespace Services.IServices
{
    public interface IMessageService : IEntityService<Message>
    {
        Task<ServiceResult<List<Message>>> Filtering(string filter);
        Task<ServiceResult<List<Message>>> Load();
        Task<ServiceResult> Add(Message message);
        Task<ServiceResult> Edit(Message message);
        Task<ServiceResult> Delete(int id);
    }
}
