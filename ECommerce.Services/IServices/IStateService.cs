﻿using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.Services.IServices;

public interface IStateService : IEntityService<State>
{
    Task<ServiceResult<List<State>>> Filtering(string filter);
    Task<ServiceResult<List<State>>> Load();
    Task<ServiceResult> Add(State state);
    Task<ServiceResult> Edit(State state);
    Task<ServiceResult> Delete(int id);
}