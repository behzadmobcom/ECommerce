using Ecommerce.Entities.HolooEntity;

namespace ECommerce.Services.IServices;

public interface IHolooAccountNumberService : IEntityService<HolooAccountNumber>
{
    List<HolooAccountNumber> HolooAccountNumbers { get; set; }
    Task Load();
}