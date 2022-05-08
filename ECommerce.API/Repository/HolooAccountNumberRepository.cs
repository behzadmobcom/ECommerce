using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.DataContext;
using API.Interface;
using Entities.HolooEntity;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class HolooAccountNumberRepository : HolooRepository<HolooAccountNumber>,IHolooAccountNumberRepository
    {
        private readonly HolooDbContext _context;
        public HolooAccountNumberRepository(HolooDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<HolooAccountNumber> GetByAccountNumberAndBankCode(string code, CancellationToken cancellationToken)
        {
            var temp = code.Split("-");
            var bankCode = temp[0];
            var accountNumber = temp[1];

            return await _context.ACOUND_N.Where(x =>
                    x.Account_N.Equals(accountNumber) && x.Bank_Code.Equals(bankCode) && x.C_Code.Equals("00000"))
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
