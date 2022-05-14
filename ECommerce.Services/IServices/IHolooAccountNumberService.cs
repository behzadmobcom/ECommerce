using Entities.HolooEntity;

namespace Services.IServices;

public interface IHolooAccountNumberService : IEntityService<HolooAccountNumber>
{
    List<HolooAccountNumber> HolooAccountNumbers { get; set; }
    Task Load();
}