using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Services.IServices;
using Entities;
using Entities.Helper;
using ECommerce.Services.IServices;
using Services.Services;

namespace ECommerce.Services.Services
{
    public class PurchaseOrderService : EntityService<PurchaseOrder>, IPurchaseOrderService
    {
        private const string Url = "api/PurchaseOrders";
        private readonly ICookieService _cookieService;
        public PurchaseOrderService(IHttpService http, ICookieService cookieService) : base(http)
        {
            _cookieService = cookieService;
        }

        public async Task<ServiceResult<PurchaseOrder>> GetByUserId()
        {
            var currentUser = _cookieService.GetCurrentUser();
            if (currentUser.Id == 0)
                return new ServiceResult<PurchaseOrder>
                {
                    Code = ServiceCode.Error
                };
            var result = await Read(Url, $"GetByUserId?userId={currentUser.Id}");

            return Return(result);

        }

        public async Task<ServiceResult> Pay(PurchaseOrder purchaseOrder)
        {
            var result =await Update(Url, purchaseOrder,"Pay");
            return Return(result);
        }

        public Task<ServiceResult> Add(PurchaseOrder purchaseOrder)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> Edit(PurchaseOrder purchaseOrder)
        {
            var result = await Update(Url, purchaseOrder);
            return Return(result);
        }
    }
}
