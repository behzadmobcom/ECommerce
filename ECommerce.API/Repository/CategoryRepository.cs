using API.DataContext;
using API.Interface;
using API.Utilities;
using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class CategoryRepository : AsyncRepository<Category>, ICategoryRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public CategoryRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<Category>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return PagedList<Category>.ToPagedList(
            await _context.Categories.Where(x => x.Name.Contains(paginationParameters.Search)).OrderBy(on => on.Id)
                .ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }

    public async Task<Category> GetByName(string name, CancellationToken cancellationToken, int? parentId = null)
    {
        return await _context.Categories.Where(x => x.Name == name && x.ParentId == parentId && x.IsActive)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> AddAll(IEnumerable<Category> categories, CancellationToken cancellationToken)
    {
        await _context.Categories.AddRangeAsync(categories, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<int>> GetIdsByUrl(string categoryUrl, CancellationToken cancellationToken)
    {
        return await _context.Categories
            .Where(x => (x.Path == categoryUrl || x.Categories.Any(y => y.Path == categoryUrl)) && x.IsActive)
            .Select(x => x.Id).ToListAsync(cancellationToken);
    }

    public async Task<CategoryViewModel?> GetByUrl(string categoryUrl, CancellationToken cancellationToken)
    {
        return await _context.Categories
            //.Where(x => (x.Path == categoryUrl || x.Categories.Any(y => y.Path == categoryUrl)) && x.IsActive)
            .Where(x => x.Path == categoryUrl && x.IsActive)
            .Select(x => new CategoryViewModel
            {
                Name = x.Name,
                Id = x.Id,
                Categories = x.Categories.Select(c => c.Id).ToList(),
                Parent = x.Parent,
                ParentId = x.ParentId,
                ProductsId = x.Products.Select(p => p.Id).ToList()
            }).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<CategoryParentViewModel>> Parents(int productId, CancellationToken cancellationToken)
    {
        var productCategory = new List<int>();
        if (productId > 0)
        {
            var temp = _context.Products.Where(x => x.Id == productId).Include(x => x.ProductCategories);
            productCategory = temp.First().ProductCategories.Select(x => x.Id).ToList();
        }

        var allCategory = await _context.Categories.Where(x => x.IsActive).ToListAsync(cancellationToken);

        var result = await Children(allCategory, productCategory, null);
        return result.OrderBy(x => x.DisplayOrder).ToList();
    }

    public async Task<List<int>> ChildrenCategory(int categoryId, CancellationToken cancellationToken)
    {
        var categoriesId = new List<int>();

        var categories = _context.Categories.Where(x => x.ParentId == categoryId);
        if (categories.Any())
            foreach (var i in categories.Select(x => x.Id))
                categoriesId.AddRange(await ChildrenCategory(i, cancellationToken));
        else
            categoriesId.Add(categoryId);

        return categoriesId;
    }

    public IQueryable<Category> GetCategoriesWithProduct(CancellationToken cancellationToken)
    {
        return _context.Categories.Include(x => x.Products);
    }

    private async Task<List<CategoryParentViewModel>> Children(List<Category> allCategory,
        List<int> productCategory, int? parentId)
    {
        var temp = new List<CategoryParentViewModel>();
        var ret = new List<CategoryParentViewModel>();
        foreach (var parent in allCategory.Where(p => p.ParentId == parentId).ToList())
        {
            if (allCategory.Any(p => p.ParentId == parent.Id))
                temp = await Children(allCategory, productCategory, parent.Id);

            ret.Add(new CategoryParentViewModel
            {
                Id = parent.Id,
                Name = parent.Name,
                Path = parent.Path,
                Depth = parent.Depth,
                Children = temp,
                Checked = productCategory.Contains(parent.Id),
                DisplayOrder = parent.DisplayOrder
            });
            temp = new List<CategoryParentViewModel>();
        }

        return ret;
    }
}