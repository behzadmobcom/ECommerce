using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Helper;
using ECommerce.Services.IServices;

namespace ECommerce.Services.IServices
{
    public interface IPurchaseOrderService: IEntityService<PurchaseOrder>
    {
        Task<ServiceResult> Pay(PurchaseOrder purchaseOrder);
        Task<ServiceResult> Add(PurchaseOrder purchaseOrder);
        Task<ServiceResult> Edit(PurchaseOrder purchaseOrder);
        Task<ServiceResult<PurchaseOrder>> GetByUserId();
    }
}
