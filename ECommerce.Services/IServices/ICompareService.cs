using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Http;

namespace Services.IServices
{
    public interface ICompareService
    {
        ServiceResult Add(HttpContext context, int productId);
        ServiceResult Remove(HttpContext context, int productId);
        ServiceResult<List<int>> Load(HttpContext context);
        Task<ServiceResult<List<ProductCompareViewModel>>> CompareList(HttpContext context);
    }
}
