using API.DataContext;
using API.Interface;
using API.Utilities;
using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class ProductRepository : AsyncRepository<Product>, IProductRepository
{
    private readonly SunflowerECommerceDbContext _context;

    public ProductRepository(SunflowerECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Product> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _context.Products.Where(x => x.Name == name).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Product> GetByUrl(string url, CancellationToken cancellationToken)
    {
        return await _context.Products.Where(x => x.Url == url)
            .Include(x => x.Brand)
            .Include(x => x.Keywords)
            .Include(t => t.Tags)
            .Include(x => x.Images)
            .Include(x => x.Prices)
            .ThenInclude(x => x.Color)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> AddAll(IEnumerable<Product?> products, CancellationToken cancellationToken)
    {
        await _context.Products.AddRangeAsync(products, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    //public async Task<IEnumerable<Product>> GetAllHolooProducts(CancellationToken cancellationToken) => await _context.Products.Where(x => x.ArticleCode != null).ToListAsync(cancellationToken);

    public async Task<Product?> AddWithRelations(ProductViewModel productViewModel, CancellationToken cancellationToken)
    {
        Product? product = productViewModel;
        //product.Prices = productViewModel.Prices;
        product.Keywords = new List<Keyword>();
        foreach (var id in productViewModel.KeywordsId) product.Keywords.Add(_context.Keywords.First(x => x.Id == id));
        product.Tags = new List<Tag>();
        foreach (var id in productViewModel.TagsId) product.Tags.Add(_context.Tags.First(x => x.Id == id));
        product.ProductCategories = new List<Category>();
        foreach (var id in productViewModel.CategoriesId)
            product.ProductCategories.Add(_context.Categories.First(x => x.Id == id));

        //product.AttributeGroupProducts = new List<ProductAttributeGroup>();
        //foreach (var id in productViewModel.Attributes.Select(x => x.AttributeGroupId).Distinct())
        //{
        //    product.AttributeGroupProducts.Add(await _context.ProductAttributeGroups.FindAsync(id, cancellationToken));
        //}

        //product.AttributeValues = new List<ProductAttributeValue>();
        //foreach (var productAttribute in productViewModel.Attributes.Where(a => a.AttributeValue.Count > 0).Select(x => x.AttributeValue))
        //{
        //    product.AttributeValues.Add(productAttribute.FirstOrDefault());
        //}

        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<Product?> EditWithRelations(ProductViewModel productViewModel,
        CancellationToken cancellationToken)
    {
        var product = await _context.Products.Where(x => x.Id == productViewModel.Id)
            .Include(nameof(Product.ProductCategories)).Include(nameof(Product.Tags)).Include(nameof(Product.Keywords))
            .Include(nameof(Product.Images)).Include(nameof(Product.AttributeValues))
            .Include(nameof(Product.AttributeGroupProducts)).FirstAsync(cancellationToken);
        product.Name = productViewModel.Name;
        //product.ArticleCode = productViewModel.ArticleCode;
        //product.ArticleCodeCustomer = productViewModel.ArticleCodeCustomer;
        product.Description = productViewModel.Description;
        //product.Exist = productViewModel.Exist;
        product.MinOrder = productViewModel.MinOrder;
        product.MaxOrder = productViewModel.MaxOrder;
        product.MinInStore = productViewModel.MinInStore;
        product.ReorderingLevel = productViewModel.ReorderingLevel;
        product.IsDiscontinued = productViewModel.IsDiscontinued;
        product.IsActive = productViewModel.IsActive;
        product.Url = productViewModel.Url;
        //product.DiscountId = productViewModel.DiscountId;
        product.HolooCompanyId = productViewModel.HolooCompanyId;
        product.BrandId = productViewModel.BrandId != 0 ? productViewModel.BrandId : null;
        product.SupplierId = productViewModel.SupplierId;
        product.StoreId = productViewModel.StoreId;
        product.Review = productViewModel.Review;

        foreach (var productKeyword in product.Keywords) product.Keywords.Remove(productKeyword);
        foreach (var id in productViewModel.KeywordsId)
            product.Keywords.Add(await _context.Keywords.FirstAsync(x => x.Id == id));

        var tags = product.Tags;
        foreach (var tag in tags) product.Tags.Remove(tag);
        foreach (var id in productViewModel.TagsId) product.Tags.Add(await _context.Tags.FirstAsync(x => x.Id == id));

        foreach (var category in product.ProductCategories) product.ProductCategories.Remove(category);
        foreach (var id in productViewModel.CategoriesId)
            product.ProductCategories.Add(await _context.Categories.FirstAsync(x => x.Id == id));

        //if (productViewModel.Attributes != null)
        //{
        //    foreach (var attributeValues in productViewModel.شفف)
        //    {
        //        product.AttributeValues.Remove(attributeValues);
        //    }

        //    foreach (var productAttribute in productViewModel.Attributes.Where(a => a.AttributeValue.Count > 0)
        //        .Select(x => x.AttributeValue))
        //    {
        //        product.AttributeValues.Add(productAttribute.FirstOrDefault());
        //    }
        //}

        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public IQueryable<Product> GetWithInclude(CancellationToken cancellationToken)
    {
        return _context.Products.AsNoTracking().Include(x => x.ProductCategories).Include(x => x.Prices)
            .Include(x => x.Brand).Include(x => x.Images);
    }

    public IQueryable<Product> GetProductByIdWithInclude(int productId)
    {
        return _context.Products.AsNoTracking().Where(x => x.Id == productId)
            .Include(c => c.ProductCategories)
            .Include(u => u.ProductUserRanks)
            .Include(p => p.Prices)
            .Include(t => t.Tags)
            .Include(k => k.Keywords)
            .Include(b => b.Brand)
            .Include(su => su.Supplier)
            .Include(st => st.Store)
            .Include(i => i.Images);
    }

    public int GetCategoryProductCount(int categoryId, CancellationToken cancellationToken)
    {
        return _context.Products.Count(x => x.ProductCategories.Any(i => i.Id == categoryId));
    }

    public async Task<List<ProductIndexPageViewModel>> GetProductList(List<int> productIdList,
        CancellationToken cancellationToken)
    {
        var productIndexPageViewModel = new List<ProductIndexPageViewModel>();
        var products = await _context.Products
            .Where(x => productIdList.Contains(x.Id))
            .Include(x => x.Brand)
            .Include(x => x.Images)
            .Include(x => x.ProductUserRanks)
            .Include(x => x.Prices)
            .ThenInclude(c=> c.Color)
            .ToListAsync(cancellationToken);
        productIndexPageViewModel.AddRange(products.Select(product => (ProductIndexPageViewModel)product));
        return productIndexPageViewModel;
    }

    public async Task<List<ProductCompareViewModel>> GetProductListWithAttribute(List<int> productIdList,
        CancellationToken cancellationToken)
    {
        var group = await _context.ProductAttributeGroups
            .Include(a => a.Attribute)
            .ToListAsync(cancellationToken);

        var productCompareViewModel = new List<ProductCompareViewModel>();
        var products = await _context.Products
            .Where(x => productIdList.Contains(x.Id)).Include(x => x.Prices)
            .Include(x => x.AttributeValues).ThenInclude(x => x.ProductAttribute)
            .Select(x => new ProductCompareViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Prices.FirstOrDefault(y => !y.IsColleague && y.MinQuantity == 1).Amount,
                ImagePath = $"{x.Images.FirstOrDefault().Path}/{x.Images.FirstOrDefault().Name}",
                Url = x.Url,
                Brand = x.Brand.Name,
                Alt = x.Images.FirstOrDefault().Alt,
                AttributeValues = x.AttributeValues.ToList(),
                AttributeGroupProducts = group
            })
            .ToListAsync(cancellationToken);

        foreach (var product in products)
        {
            _ = await _context.ProductAttributeValues.Where(x => x.ProductId == product.Id)
                .ToListAsync(cancellationToken);
        }


        //foreach (var productAttributeGroup in group)
        //foreach (var attribute in productAttributeGroup.Attribute)
        //    if (attribute.AttributeValue.Count == 0)
        //        attribute.AttributeValue.Add(new ProductAttributeValue());

        productCompareViewModel.AddRange(products.Select(product => product));
        return productCompareViewModel;
    }

    public async Task<List<Product>> GetByCategoryId(int categoryId, CancellationToken cancellationToken)
    {
        var products = await _context.Products.Where(x => x.ProductCategories.Any(y => y.Id == categoryId))
            .OrderByDescending(x => x.Id)
            .Include(x => x.Brand)
            .Include(x => x.Images)
            .Include(x => x.Prices)
            .Include(x => x.ProductUserRanks)
            .ToListAsync(cancellationToken);

        return products;
    }

    /// <summary>
    /// </summary>
    /// <param name="brandsId"></param>
    /// <param name="starsCount"></param>
    /// <param name="tagsId"></param>
    /// <returns></returns>
    public IQueryable<Product?> GetProducts(int categoryId, List<int>? brandsId, List<int>? starsCount, List<int>? tagsId)
    {

        var products = _context.Products.Where(x => x.Prices!.Any()).AsQueryable();

        if (categoryId > 0) products = products.Where(x => x.ProductCategories.Any(cat => cat.Id == categoryId));

        if (brandsId is { Count: > 0 }) products = products.Where(x => brandsId.Contains((int)x.BrandId));

        if (starsCount is { Count: > 0 })
            products = products.Where(x =>
                starsCount.Contains(x.ProductUserRanks.Sum(s => s.Stars) / x.ProductUserRanks.Count));
        if (tagsId is { Count: > 0 })
            products = tagsId.Aggregate(products,
                (current, tagId) => current.Where(x => x.Tags.Any(t => t.Id == tagId)));

        return products;
    }

    public async Task<PagedList<ProductIndexPageViewModel>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return PagedList<ProductIndexPageViewModel>.ToPagedList(
            await _context.Products.Where(x => x.Name.Contains(paginationParameters.Search))
                .AsNoTracking()
                .OrderByDescending(on => on.Id)
                .Select(p => new ProductIndexPageViewModel
                {
                    Prices = p.Prices!,
                    Alt = p.Images!.First().Alt,
                    Brand = p.Brand!.Name,
                    Name = p.Name,
                    Description = p.Description,
                    Id = p.Id,
                    ImagePath = $"{p.Images!.First().Path}/{p.Images!.First().Name}",
                    Stars = p.Star,
                    Url = p.Url
                })
                .ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }

       public async Task<Product?> GetProductById(int id, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Where(x => x.Id == id)
            .Include(x => x.Brand)
            .Include(x => x.Images)
            .Include(x => x.ProductUserRanks)
            .Include(x => x.Prices)
            .ThenInclude(c => c.Color)
            .FirstOrDefaultAsync(cancellationToken);
      
    }
    #region Tops

    public async Task<List<ProductIndexPageViewModel>> TopNew(int count, CancellationToken cancellationToken)
    {
        var products = await _context.Products.Where(x => x.Images!.Count > 0 && x.Prices!.Any()).OrderByDescending(x => x.Id).Take(count)
            .Select(p => new ProductIndexPageViewModel
            {
                Prices = p.Prices!,
                Alt = p.Images!.First().Alt,
                Brand = p.Brand!.Name,
                Name = p.Name,
                Description = p.Description,
                Id = p.Id,
                ImagePath = $"{p.Images!.First().Path}/{p.Images!.First().Name}",
                Stars = p.Star,
                Url = p.Url
            })
            .ToListAsync(cancellationToken);

        return products;
    }

    public async Task<List<ProductIndexPageViewModel>> TopPrices(int count, CancellationToken cancellationToken)
    {
        var products = await _context.Prices.OrderByDescending(x => x.Amount)
            .Where(x => x.Product.Images.Count > 0)
            .Take(count)
            .Select(p => new ProductIndexPageViewModel
            {
                Prices = p.Product!.Prices!,
                Alt = p.Product.Images!.First().Alt,
                Brand = p.Product.Brand!.Name,
                Name = p.Product.Name,
                Description = p.Product.Description,
                Id = p.Product.Id,
                ImagePath = $"{p.Product.Images!.First().Path}/{p.Product.Images!.First().Name}",
                Stars = p.Product.Star,
                Url = p.Product.Url
            })
            .ToListAsync(cancellationToken);
        return products;
    }

    public async Task<List<ProductIndexPageViewModel>> TopChip(int count, CancellationToken cancellationToken)
    {
        var products = await _context.Prices.OrderBy(x => x.Amount)
            .Where(x => x.Product!.Images!.Count > 0)
            .Take(count)
            .Select(p => new ProductIndexPageViewModel
            {
                Prices = p.Product!.Prices!,
                Alt = p.Product.Images!.First().Alt,
                Brand = p.Product.Brand!.Name,
                Name = p.Product.Name,
                Description = p.Product.Description,
                Id = p.Product.Id,
                ImagePath = $"{p.Product.Images!.First().Path}/{p.Product.Images!.First().Name}",
                Stars = p.Product.Star,
                Url = p.Product.Url
            })
            .ToListAsync(cancellationToken);
        return products;
    }

    //public async Task<List<ProductIndexPageViewModel?>> TopDiscounts(int count, CancellationToken cancellationToken)
    //{
    //    var products = new List<ProductIndexPageViewModel?>();
    //    var discounts = _context.Discounts.OrderByDescending(x => x.Amount).Select(x => x.Products).Take(count);
    //    var i = 0;
    //    foreach (var discount in discounts)
    //        foreach (var product in discount)
    //        {
    //            products.Add(await _context.Products
    //                .Where(x => x.Id == product.Id && x.Images!.Count > 0 && x.Prices!.Any())
    //                .Select(p => new ProductIndexPageViewModel
    //                {
    //                    Prices = p.Prices!,
    //                    Alt = p.Images!.First().Alt,
    //                    Brand = p.Brand!.Name,
    //                    Name = p.Name,
    //                    Description = p.Description,
    //                    Id = p.Id,
    //                    ImagePath = $"{p.Images!.First().Path}/{p.Images!.First().Name}",
    //                    Stars = p.Star,
    //                    Url = p.Url
    //                })
    //                .FirstOrDefaultAsync(cancellationToken));
    //            i++;
    //            if (i == count) break;
    //        }

    //    //if (productIndexPageViewModel.Count < 5)
    //    //{
    //    //    productIndexPageViewModel.AddRange(await TopNew(count - productIndexPageViewModel.Count, cancellationToken));
    //    //}
    //    return products;
    //}

    public async Task<List<ProductIndexPageViewModel>> TopStars(int count, CancellationToken cancellationToken)
    {
        var products = await _context.ProductUserRanks.OrderByDescending(x => x.Stars)
            .Where(x => x.Product!.Images!.Count > 0 && x.Product.Prices!.Any())
            .Take(count)
            .Select(p => new ProductIndexPageViewModel
            {
                Prices = p.Product!.Prices!,
                Alt = p.Product.Images!.First().Alt,
                Brand = p.Product.Brand!.Name,
                Name = p.Product.Name,
                Description = p.Product.Description,
                Id = p.Product.Id,
                ImagePath = $"{p.Product.Images!.First().Path}/{p.Product.Images!.First().Name}",
                Stars = p.Product.Star,
                Url = p.Product.Url
            })
            .ToListAsync(cancellationToken);

        return products;
    }

    public async Task<List<ProductIndexPageViewModel>> TopSells(int count, CancellationToken cancellationToken)
    {
        var products = await _context.ProductSellCounts.OrderByDescending(x => x.Count)
            .Where(x => x.Product!.Images!.Count > 0 && x.Product.Prices!.Any())
            .Take(count)
            .Select(p => new ProductIndexPageViewModel
            {
                Prices = p.Product!.Prices!,
                Alt = p.Product.Images!.First().Alt,
                Brand = p.Product.Brand!.Name,
                Name = p.Product.Name,
                Description = p.Product.Description,
                Id = p.Product.Id,
                ImagePath = $"{p.Product.Images!.First().Path}/{p.Product.Images!.First().Name}",
                Stars = p.Product.Star,
                Url = p.Product.Url
            })
            .ToListAsync(cancellationToken);

        return products;
    }

    public async Task<List<ProductIndexPageViewModel>> TopRelatives(int productId, int count, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories
            .Where(y => y.Products.Any(x => x.Id == productId && x.Prices.Any()))
            .ToListAsync(cancellationToken);
        var products = new List<ProductIndexPageViewModel>();
        var countPerCategory = count;
        if (categories.Count > 1)
        {
            countPerCategory = count / categories.Count;
            if (countPerCategory < 1) countPerCategory = 1;
        }

        foreach (var category in categories)
            products.AddRange( await _context.Products
                .Where(x => x.ProductCategories.Any(x => x.Id == category.Id) && x.Id != productId)
                .Take(countPerCategory)
                .Select(p => new ProductIndexPageViewModel
                {
                    Prices = p.Prices!,
                    Alt = p.Images!.First().Alt,
                    Brand = p.Brand!.Name,
                    Name = p.Name,
                    Description = p.Description,
                    Id = p.Id,
                    ImagePath = $"{p.Images!.First().Path}/{p.Images!.First().Name}",
                    Stars = p.Star,
                    Url = p.Url
                })
                .ToListAsync(cancellationToken));
        //productsList.AddRange(temp);

        return products.Take(count).ToList();
    }

    #endregion
}