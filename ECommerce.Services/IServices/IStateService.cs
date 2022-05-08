using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Entities;
using Entities.Helper;

namespace Services.IServices
{
    public interface IStateService : IEntityService<State>
    {
        Task<ServiceResult<List<State>>> Filtering(string filter);
        Task<ServiceResult<List<State>>> Load();
        Task<ServiceResult> Add(State state);
        Task<ServiceResult> Edit(State state);
        Task<ServiceResult> Delete(int id);
    }
}
