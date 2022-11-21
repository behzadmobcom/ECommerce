using Ecommerce.Entities;

namespace ECommerce.API.Interface;

public interface ISettingRepository : IAsyncRepository<Setting>
{
    string IsDollar();
}