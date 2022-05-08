using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Helper;

namespace Services.IServices
{
   public interface ISendInformationService
    {
        Task<ServiceResult<List<SendInformation>>> Filtering(string filter,int id);
        Task<ServiceResult<List<SendInformation>>> Load(int id);
        Task<ServiceResult<SendInformation>> Find(int id);
        Task<ServiceResult> Add(SendInformation sendInformation);
        Task<ServiceResult> Edit(SendInformation sendInformation);
        Task<ServiceResult> Delete(int id);
    }
}
