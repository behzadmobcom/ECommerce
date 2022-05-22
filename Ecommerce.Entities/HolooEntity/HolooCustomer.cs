using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.HolooEntity
{
    public class HolooCustomer
    {
        public string C_Code { get; set; }
        public string C_Name { get; set; }
        public string C_Code_C { get; set; }
        public string C_Mobile { get; set; }
        public double Sum_Takhfif { get; set; }
        public double Etebar { get; set; }
        public double First_BalanceSanad { get; set; }
        public bool HlpAman { get; set; }
        public string Col_Code_Bed { get; set; }
        public string Moien_Code_Bed { get; set; }
        public string Tafzili_Code_Bed { get; set; }
    }
}
