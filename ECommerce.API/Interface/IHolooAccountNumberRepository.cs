using Ecommerce.Entities.HolooEntity;

namespace ECommerce.API.Interface;

public interface IHolooAccountNumberRepository : IHolooRepository<HolooAccountNumber>
{
    Task<HolooAccountNumber> GetByAccountNumberAndBankCode(string code, CancellationToken cancellationToken);
}