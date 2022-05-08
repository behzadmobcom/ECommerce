

using Services.IServices;
using Entities.HolooEntity;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services
{
    public class HolooAccountNumberService : EntityService<HolooAccountNumber>, IHolooAccountNumberService
    {
        public HolooAccountNumberService(IHttpService http) : base(http)
        {
        }
        private const string Url = "api/PaymentMethods/HolooAccount";
        public List<HolooAccountNumber> HolooAccountNumbers { get; set; }
        public async Task Load()
        {
            HolooAccountNumbers = (await ReadList(Url)).ReturnData;
        }
    }
}
