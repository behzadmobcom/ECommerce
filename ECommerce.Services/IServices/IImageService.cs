using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Services.IServices;

public interface IImageService : IEntityService<Image>
{
    Task<ServiceResult> Add(IFormFile file, int entityId, string path, string contentRootPath);
    Task<ServiceResult> Edit(IFormFile file, Image image, string path, string contentRootPath);
    Task<ServiceResult> Delete(string imageName, int id, string contentRootPath);
    Task<ServiceResult<List<Image>>> GetImagesByProductId(int productId);
    Task<ServiceResult<List<string>>> Upload(IFormFile file, string path, string contentRootPath);
    Task<ServiceResult<Image>> GetImagesByBlogId(int blogId);
}