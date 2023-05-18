using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.ViewModel;
public class CardResultViewModel
{
    public string? CartList { get; set; }
    public int CartCount { get; set; }
    public decimal AllPrice { get; set; }
}
