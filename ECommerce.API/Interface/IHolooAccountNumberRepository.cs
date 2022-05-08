using System.Threading;
using System.Threading.Tasks;
using Entities.HolooEntity;

namespace API.Interface
{
    public interface IHolooAccountNumberRepository : IHolooRepository<HolooAccountNumber>
    {
        Task<HolooAccountNumber> GetByAccountNumberAndBankCode(string code, CancellationToken cancellationToken);
    }
}
