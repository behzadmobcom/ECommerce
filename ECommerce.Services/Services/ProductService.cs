using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.Extensions.Caching.Memory;

namespace ECommerce.Services.Services;

public class ProductService : EntityService<ProductViewModel>, IProductService
{
    private const string Url = "api/Products";
    private readonly ICategoryService _categoryService;
    private readonly IHttpService _http;
    private readonly IImageService _imageService;
    private readonly IKeywordService _keywordService;
    private readonly ITagService _tagService;
    private readonly IMemoryCache _cache;

    public ProductService(IHttpService http, ITagService tagService, IImageService imageService,
        IKeywordService keywordService, ICategoryService categoryService, IMemoryCache cach) : base(http)
    {
        _http = http;
        _tagService = tagService;
        _imageService = imageService;
        _keywordService = keywordService;
        _categoryService = categoryService;
        _cache = cach;
    }

    public async Task<ServiceResult<ProductViewModel>> FillProductEdit(ProductViewModel productViewModel)
    {
        var keywords = await _keywordService.GetKeywordsByProductId(productViewModel.Id);
        productViewModel.KeywordsId = keywords.ReturnData.Select(x => x.Id).ToList();

        var tags = await _tagService.GetTagsByProductId(productViewModel.Id);
        productViewModel.TagsId = tags.ReturnData.Select(x => x.Id).ToList();

        var images = await _imageService.GetImagesByProductId(productViewModel.Id);
        productViewModel.Images = images.ReturnData;

        var categories = await _categoryService.GetCategoriesByProductId(productViewModel.Id);
        productViewModel.CategoriesId = categories.ReturnData.Select(x => x.Id).ToList();

        return new ServiceResult<ProductViewModel>
        {
            Code = ServiceCode.Success,
            ReturnData = productViewModel
        };
    }

    public async Task<ServiceResult<ProductViewModel>> GetProduct(string productUrl)
    {
        var cachEntry = await _cache.GetOrCreate("GetProduct", async entry =>
        {
            var result = await _http.GetAsync<ProductViewModel>(Url, $"GetByProductUrl?productUrl={productUrl}");
            return result;
        });

        return Return(cachEntry);
    }

    public ServiceResult CheckBeforeSend(ProductViewModel product)
    {
        if (product.TagsId.Count == 0)
            return new ServiceResult
            {
                Code = ServiceCode.Warning,
                Message = "لطفا ابتدا تگ ها را وارد کنید"
            };
        if (product.KeywordsId.Count == 0)
            return new ServiceResult
            {
                Code = ServiceCode.Warning,
                Message = "لطفا ابتدا کلمات کلیدی را وارد کنید"
            };
        if (product.Prices.Count == 0)
            return new ServiceResult
            {
                Code = ServiceCode.Warning,
                Message = "لطفا ابتدا لیست قیمت را وارد کنید"
            };

        if (product.Prices.All(x => x.MinQuantity != 1))
            return new ServiceResult
            {
                Code = ServiceCode.Warning,
                Message = "لطفا حتما یکی از قیمت ها از تعداد 1 شروع شود"
            };

        if (product.CategoriesId.Count == 0)
            return new ServiceResult
            {
                Code = ServiceCode.Warning,
                Message = "لطفا ابتدا دسته بندی را کنید"
            };

        if (product.Attributes == null || product.Attributes.Count == 0)
            return new ServiceResult
            {
                Code = ServiceCode.Warning,
                Message = "لطفا ابتدا مشخصات را کنید"
            };
        return new ServiceResult
        {
            Code = ServiceCode.Success
        };
    }

    public async Task<ServiceResult<ProductViewModel>> Add(ProductViewModel productViewModel)
    {
        var cachEntry = await _cache.GetOrCreate("Add", async entry =>
        {
            if (productViewModel.BrandId == 0) productViewModel.BrandId = null;
            var result = await Create<ProductViewModel>(Url, productViewModel);
            return result;
        });

        return Return(cachEntry);
    }

    public async Task<ServiceResult> Edit(ProductViewModel productViewModel)
    {
        var cachEntry = await _cache.GetOrCreate("Edit", async entry =>
        {
            var result = await UpdateWithReturnId(Url, productViewModel);
            return result;
        });

        return Return(cachEntry);

    }

    public async Task<ServiceResult> Delete(int id)
    {
        var cachEntry = await _cache.GetOrCreate("Delete", async entry =>
        {
            var result = await Delete(Url, id);
            return result;
        });

        return Return(cachEntry);

    }

    public async Task<ServiceResult<List<ProductIndexPageViewModel>>> Search(string search = "", int pageNumber = 0, int pageSize = 9)
    {
        var cachEntry = await _cache.GetOrCreate("Search", async entry =>
        {
            var result = await _http.GetAsync<List<ProductIndexPageViewModel>>(Url,
             $"GetAllWithPagination?PageNumber={pageNumber}&Search={search}&PageSize={pageSize}");
            return result;
        });

        return Return(cachEntry);

    }

    //public async Task<ServiceResult<PaginationViewModel>> Search(string searchText, int page, int quantityPerPage = 9)
    //{
    //    var pageViewModel = new PageViewModel
    //    {
    //        Page = page,
    //        QuantityPerPage = quantityPerPage,
    //        SearchText = searchText
    //    };
    //    var result = await _http.PostAsync<PageViewModel, PaginationViewModel>(Url, pageViewModel, "Search");
    //    return Return(result);
    //}

    public async Task<ServiceResult<List<ProductIndexPageViewModel>>> TopProducts(string CategoryId = "", string search = "",
        int pageNumber = 0, int pageSize = 10, int productSort = 1, int? endPrice = null, int? startPrice = null,
        bool isExist = false, bool isWithoutBail = false, string tagText = "")
    {
        //var result = await _http.GetAsync<List<ProductIndexPageViewModel>>(Url, $"NewProducts?count={count}");
        //return Return<List<ProductIndexPageViewModel>>(result);
        var cachEntry = await _cache.GetOrCreate("TopProducts", async entry =>
        {
            var command = "GetProducts?" +
                          $"PaginationParameters.PageNumber={pageNumber}&" +
                           $"IsWithoutBail={isWithoutBail}&" +
                          $"PaginationParameters.PageSize={pageSize}&";
            if (!string.IsNullOrEmpty(search)) command += $"PaginationParameters.Search={search}&";
            if (!string.IsNullOrEmpty(CategoryId)) command += $"PaginationParameters.CategoryId={CategoryId}&";
            if (!string.IsNullOrEmpty(tagText)) command += $"PaginationParameters.TagText={tagText}&";
            if (startPrice != null) command += $"StartPrice={startPrice}&";
            if (endPrice != null) command += $"EndPrice={endPrice}&";
            command += $"IsExist={isExist}&";
            command += $"ProductSort={productSort}";
            var result = await _http.GetAsync<List<ProductIndexPageViewModel>>(Url, command);
            return result;
        });


        return Return(cachEntry);
    }


    public async Task<ServiceResult<List<ProductIndexPageViewModel>>> GetProductList(int categoryId, List<int> brandsId,
        int starCount, int tagId, int pageNumber = 0, int pageSize = 12, int productSort = 1)
    {
        var cachEntry = await _cache.GetOrCreate("GetProductList", async entry =>
        {
            var result = await _http.GetAsync<List<ProductIndexPageViewModel>>(Url,
            $"GetByCategoryId?PageNumber={pageNumber}&PageSize={pageSize}&Search={categoryId}&ProductSort={productSort}");
            return result;
        });

        return Return(cachEntry);

    }

    public async Task<ServiceResult<List<ProductIndexPageViewModel>>> TopNew(int count, bool isWithoutBail = false)
    {
        var cachEntry = await _cache.GetOrCreate("TopNew", async entry =>
        {
            var result = await _http.GetAsync<List<ProductIndexPageViewModel>>(Url, $"TopNew?count={count}");
            return result;
        });

        return Return(cachEntry);

    }

    public async Task<ServiceResult<List<ProductIndexPageViewModel>>> TopNewShop(int count, bool isWithoutBail = false)
    {
        var cachEntry = await _cache.GetOrCreate("TopNewShop", async entry =>
        {
            var result = await _http.GetAsync<List<ProductIndexPageViewModel>>(Url, $"TopNew?count={count}");
            return result;
        });

        return Return(cachEntry);

    }
    public async Task<ServiceResult<List<ProductIndexPageViewModel>>> TopPrice(int count, bool isWithoutBail = false)
    {
        var cachEntry = await _cache.GetOrCreate("TopPrice", async entry =>
        {
            var result = await _http.GetAsync<List<ProductIndexPageViewModel>>(Url, $"TopPrice?count={count}");
            return result;
        });

        return Return(cachEntry);

    }
    public async Task<ServiceResult<List<ProductIndexPageViewModel>>> TopStars(int count, bool isWithoutBail = false)
    {
        var cachEntry = await _cache.GetOrCreate("TopStars", async entry =>
        {
            var result = await _http.GetAsync<List<ProductIndexPageViewModel>>(Url, $"TopStars?count={count}");
            return result;
        });

        return Return(cachEntry);

    }
    public async Task<ServiceResult<List<ProductIndexPageViewModel>>> TopDiscount(int count, bool isWithoutBail = false)
    {
        var cachEntry = await _cache.GetOrCreate("TopDiscount", async entry =>
        {
            var result = await _http.GetAsync<List<ProductIndexPageViewModel>>(Url, $"TopDiscount?count={count}");
            return result;
        });

        return Return(cachEntry);

    }
    public async Task<ServiceResult<List<ProductIndexPageViewModel>>> TopRelatives(int productId, int count, bool isWithoutBail = false)
    {
        var cachEntry = await _cache.GetOrCreate("TopRelatives", async entry =>
        {
            var result =
             await _http.GetAsync<List<ProductIndexPageViewModel>>(Url,
                 $"TopRelatives?productId={productId}&count={count}");
            return result;
        });

        return Return(cachEntry);

    }

    public async Task<ServiceResult<List<ProductIndexPageViewModel>>> TopSells(int count, bool isWithoutBail = false)
    {
        var cachEntry = await _cache.GetOrCreate("TopSells", async entry =>
        {
            var result = await _http.GetAsync<List<ProductIndexPageViewModel>>(Url, $"TopSells?count={count}");
            return result;
        });

        return Return(cachEntry);

    }

    public async Task<ServiceResult<List<ProductIndexPageViewModel>>> ProductsWithIdsForCart(List<int> productIdList)
    {
        var cachEntry = await _cache.GetOrCreate("ProductsWithIdsForCart", async entry =>
        {
            var result =
           await _http.PostAsync<List<int>, List<ProductIndexPageViewModel>>("api/Products", productIdList,
               "ProductsWithIdsForCart");
            return result;
        });

        return Return(cachEntry);

    }

    public async Task<ServiceResult<List<ProductCompareViewModel>>> ProductsWithIdsForCompare(List<int> productIdList)
    {
        var cachEntry = await _cache.GetOrCreate("ProductsWithIdsForCompare", async entry =>
        {
            var result =
           await _http.PostAsync<List<int>, List<ProductCompareViewModel>>("api/Products", productIdList,
               "ProductsWithIdsForCompare");
            return result;
        });

        return Return(cachEntry);

    }

    public async Task<ServiceResult<ProductViewModel>> GetById(int id)
    {
        var cachEntry = await _cache.GetOrCreate("GetById", async entry =>
        {
            var result = await _http.GetAsync<ProductViewModel>(Url, $"GetById?id={id}");
            return result;
        });

        return Return(cachEntry);

    }

    public async Task<ServiceResult<ProductModalViewModel>> GetByIdViewModel(int id)
    {
        var cachEntry = await _cache.GetOrCreate("GetByIdViewModel", async entry =>
        {
            var result = await _http.GetAsync<ProductModalViewModel>(Url, $"GetByIdViewModel?id={id}");
            return result;
        });

        return Return(cachEntry);

    }


    public async Task<ServiceResult<List<ProductIndexPageViewModel>>> GetTops(string includeProperties, bool isWithoutBail = false)
    {
        var cachEntry = await _cache.GetOrCreate("GetTops", async entry =>
        {
            var result = await _http.GetAsync<List<ProductIndexPageViewModel>>(Url, $"GetTops?includeProperties={includeProperties}");
            return result;
        });

        return Return(cachEntry);

    }
}