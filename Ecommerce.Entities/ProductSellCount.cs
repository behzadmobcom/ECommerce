using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities
{
    public class ProductSellCount : BaseEntity
    {
        public int ProductId { get; set; }

        [Display(Name = "تعداد فروش")]
        public int Count { get; set; }

        public Product? Product { get; set; }
    }
}
