using System.Collections.Generic;

namespace Entities.ViewModel
{
    public class TagProductId
    {
        public int Id { get; set; } 
        public IEnumerable<int> ProductsId { get; set; }
    }
}
