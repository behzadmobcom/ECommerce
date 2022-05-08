using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.HolooEntity;

namespace Entities.ViewModel
{
    public class FactorViewModel
    {
        public HolooFBail HolooFBail { get; set; }
        public List<HolooABail> HolooABails { get; set; }
    }
}
