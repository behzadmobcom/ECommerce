using API.DataContext;
using API.Interface;
using API.Utilities;
using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class TagRepository : AsyncRepository<Tag>, ITagRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public TagRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<Tag>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return PagedList<Tag>.ToPagedList(
            await _context.Tags.Where(x => x.TagText.Contains(paginationParameters.Search)).AsNoTracking()
                .OrderBy(on => on.Id).ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }

    public async Task<Tag> GetByTagText(string tagText, CancellationToken cancellationToken)
    {
        return await _context.Tags.Where(x => x.TagText == tagText).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<TagProductId>> GetByProductId(int productId, CancellationToken cancellationToken)
    {
        return await _context.Tags.Where(x => x.Products.Any(y => y.Id == productId))
            .Select(x => new TagProductId {Id = x.Id, ProductsId = x.Products.Select(x => x.Id)})
            .ToListAsync(cancellationToken);
    }
}