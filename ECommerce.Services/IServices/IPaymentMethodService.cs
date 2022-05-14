using Entities;
using Entities.Helper;

namespace Services.IServices;

public interface IPaymentMethodService : IEntityService<PaymentMethod>
{
    Task<ServiceResult<List<PaymentMethod>>> Filtering(string filter);
    Task<ServiceResult<List<PaymentMethod>>> Load(string search = "", int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult> Add(PaymentMethod paymentMethod);
    Task<ServiceResult> Edit(PaymentMethod paymentMethod);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<PaymentMethod>> GetById(int id);
}