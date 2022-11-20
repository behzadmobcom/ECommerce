using Ecommerce.Entities;

namespace ECommerce.API.Interface;

public interface ISlideShowRepository : IAsyncRepository<SlideShow>
{
    bool IsRepetitiveProduct(int id,int productId, CancellationToken cancellationToken);

    Task<SlideShow> GetByTitle(string title, CancellationToken cancellationToken);

    Task<IEnumerable<SlideShow>> GetAllWithInclude(CancellationToken cancellationToken);
}