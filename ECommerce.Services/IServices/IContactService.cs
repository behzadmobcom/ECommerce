using Ecommerce.Entities;
using Ecommerce.Entities.Helper;

namespace ECommerce.Services.IServices;

public interface IContactService : IEntityService<Contact>
{
    Task<ServiceResult<List<Contact>>> GetAll(string search = "", int pageNumber = 0, int pageSize = 10);
    Task<ServiceResult> Add(Contact contact);
    Task<ServiceResult> Edit(Contact contact);
    Task<ServiceResult> Delete(int id);
    Task<ServiceResult<Contact>> GetById(int id);
    Task<ServiceResult<Contact?>> GetByName(string name);
    Task<ServiceResult<Contact?>> GetByEmail(string email);
}