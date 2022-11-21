using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repository;

public class ProductAttributeRepository : AsyncRepository<ProductAttribute>, IProductAttributeRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public ProductAttributeRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<ProductAttribute>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return PagedList<ProductAttribute>.ToPagedList(
            await _context.ProductAttributes.Where(x => x.Title.Contains(paginationParameters.Search)).AsNoTracking()
                .OrderBy(on => on.Id).ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }

    public async Task<ProductAttribute> GetByTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.ProductAttributes.Where(x => x.Title == title).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProductAttribute>> GetAll(int productId, CancellationToken cancellationToken)
    {
        //var productAttributes = await _context.ProductAttributes.Where(x => x.AttributeValue.Any(p => p.ProductId == productId)).Include(x=>x.AttributeValue).ToListAsync();
        var productAttributes = await _context.ProductAttributes.ToListAsync(cancellationToken);
        var productValues = await _context.ProductAttributeValues.Where(x => x.ProductId == productId)
            .ToListAsync(cancellationToken);
        //foreach (var productValue in productValues)
        //{
        //    productAttributes.First(x => x.Id == productValue.ProductAttributeId).AttributeValue.Add(productValue);
        //}

        return productAttributes;
    }

    public async Task<IEnumerable<ProductAttribute>> GetAllAttributeWithGroupId(int groupId, int productId,
        CancellationToken cancellationToken)
    {
        var productAttributes = await _context.ProductAttributes.Where(x => x.AttributeGroupId == groupId)
            .Include(i => i.AttributeValue)
            .Select(s => new ProductAttribute
            {
                Id = s.Id, AttributeValue = s.AttributeValue.Where(v => v.ProductId == productId).ToList(),
                AttributeGroup = s.AttributeGroup, AttributeGroupId = s.AttributeGroupId,
                AttributeType = s.AttributeType, Title = s.Title
            })
            .ToListAsync(cancellationToken);
        return productAttributes;
    }
}