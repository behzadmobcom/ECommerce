using System.Collections.Generic;

namespace Entities.ViewModel
{
   public class PaginationViewModel
   {
       public int Page { get; set; } = 1;
       public int QuantityPerPage { get; set; } = 9;
       public string? SearchText { get; set; }
       public int PagesQuantity { get; set; }
       public int ProductsCount { get; set; } = 0;
       public List<Product>? Products { get; set; }
   }
}
