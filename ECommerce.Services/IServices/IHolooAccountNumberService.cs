using Entities.HolooEntity;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IHolooAccountNumberService : IEntityService<HolooAccountNumber>
    {
        List<HolooAccountNumber> HolooAccountNumbers { get; set; }
        Task Load();
    }
}
