using Ecommerce.Entities.HolooEntity;

namespace ECommerce.API.Interface
{
    public interface IHolooCustomerRepository : IHolooRepository<HolooCustomer>
    {
        Task<string> Add(HolooCustomer customer, CancellationToken cancellationToken);
        Task<string> GetNewCustomerCode();

        Task<HolooCustomer> GetCustomerByCode(string customerCode);
    }
}
