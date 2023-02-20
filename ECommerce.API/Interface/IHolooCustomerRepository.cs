using Ecommerce.Entities.HolooEntity;

namespace ECommerce.API.Interface
{
    public interface IHolooCustomerRepository : IHolooRepository<HolooCustomer>
    {
        Task<string> Add(HolooCustomer customer, CancellationToken cancellationToken);
        Task<(string customerCode, string customerCodeC)> GetNewCustomerCode();

        Task<HolooCustomer> GetCustomerByCode(string customerCode);
    }
}
