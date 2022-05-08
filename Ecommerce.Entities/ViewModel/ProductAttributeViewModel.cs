using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class ProductAttributeViewModel
    {
        public int Id { get; set; }
        public int ValueId { get; set; }
        public string? Title { get; set; }
        public string? Value { get; set; }
    }
}
