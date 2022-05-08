using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Helper
{
   public  class PaginationParameters
    {
        const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public string? Search { get; set; }
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }

   public class PaginationDetails
   {
       public string? Search { get; set; }
       public int CurrentPage { get; set; }
       public int TotalPages { get;  set; }
       public int PageSize { get;  set; }
       public int TotalCount { get;  set; }
       public bool HasPrevious { get; set; }
       public bool HasNext { get; set; }
       public string? Address { get; set; }
    }
}
