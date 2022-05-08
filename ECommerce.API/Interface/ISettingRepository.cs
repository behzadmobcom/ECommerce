using Entities;

namespace API.Interface
{
    public interface ISettingRepository : IAsyncRepository<Setting>
    {
        string IsDollar();
    }
}
