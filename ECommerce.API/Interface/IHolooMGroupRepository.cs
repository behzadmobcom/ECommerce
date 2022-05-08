using System.Threading;
using System.Threading.Tasks;

using Entities.HolooEntity;

namespace API.Interface
{
    public interface IHolooMGroupRepository : IHolooRepository<HolooMGroup>
    {
        Task<HolooMGroup> GetByCode(string code, CancellationToken cancellationToken);
    }
}
