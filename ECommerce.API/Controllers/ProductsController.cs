using ECommerce.API.Interface;
using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.HolooEntity;
using Ecommerce.Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IHolooArticleRepository _articleRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<ProductsController> _logger;
    private readonly IHolooMGroupRepository _mGroupRepository;
    private readonly IPriceRepository _priceRepository;
    private readonly IProductRepository _productRepository;
    private readonly IHolooSGroupRepository _sGroupRepository;
    private readonly IUnitRepository _unitRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IWishListRepository _wishListRepository;

    public ProductsController(IProductRepository productRepository, IHolooArticleRepository articleRepository,
        IHolooMGroupRepository mGroupRepository, IHolooSGroupRepository sGroupRepository,
        ICategoryRepository categoryRepository, IPriceRepository priceRepository, IUnitRepository unitRepository,
        ILogger<ProductsController> logger, ITagRepository tagRepository, IWishListRepository wishListRepository)
    {
        _productRepository = productRepository;
        _articleRepository = articleRepository;
        _mGroupRepository = mGroupRepository;
        _sGroupRepository = sGroupRepository;
        _categoryRepository = categoryRepository;
        _priceRepository = priceRepository;
        _unitRepository = unitRepository;
        _logger = logger;
        _tagRepository = tagRepository;
        _wishListRepository = wishListRepository;
    }

    private async Task<Product> AddPriceAndExistFromHoloo(Product product, bool isWithoutBill, bool? isCheckExist, CancellationToken cancellationToken)
    {
        var products = await _articleRepository.AddPriceAndExistFromHolooList(
            new List<ProductIndexPageViewModel> { product },
            isWithoutBill,
            isCheckExist,
            cancellationToken);
        product.Prices = products.First().Prices;
        return product;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWithPagination([FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
            var entity = await _productRepository.Search(paginationParameters, cancellationToken);
            var paginationDetails = new PaginationDetails
            {
                TotalCount = entity.TotalCount,
                PageSize = entity.PageSize,
                CurrentPage = entity.CurrentPage,
                TotalPages = entity.TotalPages,
                HasNext = entity.HasNext,
                HasPrevious = entity.HasPrevious,
                Search = paginationParameters.Search
            };



            return Ok(new ApiResult
            {
                PaginationDetails = paginationDetails,
                Code = ResultCode.Success,
                ReturnData = entity
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Search([FromBody] PageViewModel pageViewModel, CancellationToken cancellationToken)
    {
        try
        {
            var pagination = new PaginationViewModel { SearchText = pageViewModel.SearchText };
            var query = _productRepository.TableNoTracking.OrderByDescending(x => x.Id);

            var cox = query.Count();
            if (string.IsNullOrEmpty(pageViewModel.SearchText))
            {
                pagination.Products = await query.Paginate(pageViewModel);
                pagination.ProductsCount = query.Count();
                pagination.Page = pageViewModel.Page;
                pagination.QuantityPerPage = pageViewModel.QuantityPerPage;
                var temp = (double)pagination.ProductsCount / pageViewModel.QuantityPerPage;
                pagination.PagesQuantity = (int)Math.Ceiling(temp);

                //if (pagination.Products.Any(x => x.Prices.Any(p => p.ArticleCode != null)))
                //{
                //    pagination.Products = await AddPriceAndExistFromHolooList(pagination.Products);
                //}
                return Ok(new ApiResult
                {
                    Code = ResultCode.Success,
                    ReturnData = pagination
                });
            }

            //if (pageViewModel.SearchText.StartsWith("@"))
            //{
            pagination.QuantityPerPage = pageViewModel.QuantityPerPage;
            pagination.Products = await query.Where(x => x.Name.Contains(pageViewModel.SearchText.Substring(1)))
                .Paginate(pageViewModel);
            pagination.ProductsCount = query.Count(x => x.Name.Contains(pageViewModel.SearchText.Substring(1)));
            pagination.PagesQuantity =
                (int)Math.Ceiling((double)(pagination.ProductsCount / pageViewModel.QuantityPerPage));
            pagination.Page = pageViewModel.Page;

            //if (pagination.Products.Any(x => x.Prices.Any(p => p.ArticleCode != null)))
            //{
            //    pagination.Products = await AddPriceAndExistFromHolooList(pagination.Products);
            //}
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = pagination
            });
            //}
            ////var categoriesId = _categoryRepository.GetIdsByUrl(paginationViewModel.SearchText);
            //var productList = query.Where(x => x.ProductCategories.Any(y => y.Path == pageViewModel.SearchText));
            //if(!productList.Any())
            //    return Ok(new ApiResult
            //    {
            //        Code = ResultCode.NotFound
            //    });
            //pagination.QuantityPerPage = pageViewModel.QuantityPerPage;
            //pagination.Page = pageViewModel.Page;
            //pagination.Products =await productList.Paginate(pageViewModel);
            //pagination.ProductsCount = query.Count(x => Enumerable.Any<Category>(x.ProductCategories, y => y.Path == pageViewModel.SearchText));
            //pagination.PagesQuantity = (int)Math.Ceiling((double)(pagination.ProductsCount / pageViewModel.QuantityPerPage));
            //if (pagination.Products.Count == 0) return NotFound("دسته بندی موجود نیست");
            //return Ok(new ApiResult
            //{
            //    Code = ResultCode.Success,
            //    ReturnData = pagination
            //});
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="productListFilteredViewModel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] ProductListFilteredViewModel productListFilteredViewModel, CancellationToken cancellationToken)
    {
        try
        {
            //var products = await _productRepository.TopNew(Convert.ToInt32(productListFilteredViewModel.PaginationParameters.Search), cancellationToken);

            if (!string.IsNullOrEmpty(productListFilteredViewModel.PaginationParameters.TagText))
            {
                var resultTags = await _tagRepository.GetByTagText(productListFilteredViewModel.PaginationParameters.TagText, cancellationToken);
                productListFilteredViewModel.TagsId = new List<int> { resultTags.Id };
            }

            var categoriesId = new List<int>();
            categoriesId = await _categoryRepository.ChildrenCategory(productListFilteredViewModel.PaginationParameters.CategoryId,
                    cancellationToken);
            categoriesId.Add(productListFilteredViewModel.PaginationParameters.CategoryId);
            var productQuery = _productRepository.GetProducts(categoriesId, productListFilteredViewModel.BrandsId,
                productListFilteredViewModel.StarsCount, productListFilteredViewModel.TagsId);

            var search = productListFilteredViewModel.PaginationParameters.Search?.Split('='); //name=ali
            var productIndexPageViewModel = new List<ProductIndexPageViewModel>();
            if (search is { Length: > 1 })
            {
                switch (search[0].ToLower())
                {
                    case "brandid":
                        productIndexPageViewModel.AddRange(await productQuery
                            .Where(x => x.BrandId == Convert.ToInt32(search[1]))
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
                        break;
                    case "name":
                        var searchWorlds = new List<string>() { search[1].Trim() };
                        searchWorlds.AddRange(search[1].Trim().Split(' '));
                        foreach (var searchWorld in searchWorlds)
                        {
                            productIndexPageViewModel.AddRange(await productQuery
                                .Where(x => x.Name.Contains(searchWorld))
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
                        }
                        break;
                }

            }
            else
            {
                productIndexPageViewModel = await productQuery
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
            }

            if (productIndexPageViewModel.Any(x => x.Prices.Any(p => p.ArticleCode != null)))
            {
                productIndexPageViewModel = await _articleRepository.AddPriceAndExistFromHolooList(productIndexPageViewModel, productListFilteredViewModel.isWithoutBill, productListFilteredViewModel.isCheckExist, cancellationToken);
            }

            if (productListFilteredViewModel.EndPrice > 0)
            {
                productIndexPageViewModel = productIndexPageViewModel.Where(x => x.Prices.Max(p => p.Amount) >= productListFilteredViewModel.StartPrice && x.Prices.Max(p => p.Amount) <= productListFilteredViewModel.EndPrice).ToList();
            }

            if (productListFilteredViewModel.isCheckExist != null)
            {
                var s = productIndexPageViewModel.Where(x => x.Prices.Any(e => e.Exist == 0)).ToList();
            }

            if (search != null && !search[0].ToLower().Equals("name"))
            {
                switch (productListFilteredViewModel.ProductSort)
                {
                    case ProductSort.New:
                        productIndexPageViewModel = productIndexPageViewModel.OrderByDescending(x => x.Id).ToList();
                        break;
                    case ProductSort.Star:
                        productIndexPageViewModel = productIndexPageViewModel.OrderByDescending(x => x.Stars).ToList();
                        break;
                    case ProductSort.HighToLowPrice:
                        productIndexPageViewModel = productIndexPageViewModel
                            .OrderByDescending(x => x.Prices.Max(p => p.Amount)).ToList();
                        break;
                    case ProductSort.LowToHighPrice:
                        productIndexPageViewModel = productIndexPageViewModel.OrderBy(x => x.Prices.Min(p => p.Amount))
                            .ToList();
                        break;
                    case ProductSort.Bestsellers:
                        productIndexPageViewModel = productIndexPageViewModel.OrderBy(x => x.Prices.Max(p => p.Amount))
                            .ToList();
                        break;
                }
            }

            productIndexPageViewModel = productIndexPageViewModel.OrderByDescending(x => x.Prices.Any(e => e.Exist > 0)).ToList();

            var entity = PagedList<ProductIndexPageViewModel>.ToPagedList(productIndexPageViewModel,
                productListFilteredViewModel.PaginationParameters.PageNumber,
                productListFilteredViewModel.PaginationParameters.PageSize);

            var paginationDetails = new PaginationDetails
            {
                TotalCount = entity.TotalCount,
                PageSize = entity.PageSize,
                CurrentPage = entity.CurrentPage,
                TotalPages = entity.TotalPages,
                HasNext = entity.HasNext,
                HasPrevious = entity.HasPrevious,
                Search = productListFilteredViewModel.PaginationParameters.Search
            };

            if (productListFilteredViewModel.UserId != null && productListFilteredViewModel.UserId != 0)
            {
                foreach (var item in entity)
                {
                    var price = item.Prices.OrderBy(p => p.Amount).FirstOrDefault(x => !x.IsColleague);
                    var wishLists = await _wishListRepository.Where(x => x.UserId == productListFilteredViewModel.UserId && x.PriceId == price.Id, cancellationToken);
                    item.FirstPriceWichlist = wishLists.Any();
                }
            }

            await Task.Delay(20000, cancellationToken);

            return Ok(new ApiResult
            {
                PaginationDetails = paginationDetails,
                Code = ResultCode.Success,
                ReturnData = entity
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message); return Ok(new ApiResult { PaginationDetails = new PaginationDetails(), Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts(bool isWithoutBill, bool? isCheckExist, CancellationToken cancellationToken)
    {
        try
        {
            var productQuery = _productRepository.GetAllProducts();
            var productIndexPageViewModel = new List<ShopPageViewModel>();
            productIndexPageViewModel.AddRange(await productQuery
                           .Select(p => new ShopPageViewModel
                           {
                               Prices = p.Prices!,
                               Alt = p.Images!.First().Alt,
                               Brand = p.Brand!.Name,
                               Name = p.Name,
                               Description = p.Description,
                               Id = p.Id,
                               ImagePath = $"{p.Images!.First().Path}/{p.Images!.First().Name}",
                               Stars = p.Star,
                               Url = p.Url,
                               TagsId = p.Tags.Select(x => x.Id).ToList(),
                               CategoriesId = p.ProductCategories.Select(x => x.Id).ToList(),
                               BrandId = p.BrandId

                           })
                           .ToListAsync(cancellationToken));

            if (productIndexPageViewModel.Any(x => x.Prices.Any(p => p.ArticleCode != null)))
            {
                productIndexPageViewModel = await _articleRepository.AddPriceAndExistFromHolooList(productIndexPageViewModel, isWithoutBill, isCheckExist, cancellationToken);
            }
            productIndexPageViewModel = productIndexPageViewModel.OrderByDescending(x => x.Prices.Any(e => e.Exist > 0)).ToList();
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = productIndexPageViewModel
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message); return Ok(new ApiResult { PaginationDetails = new PaginationDetails(), Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpGet]
    public async Task<IActionResult> TopNew(int count, bool isWithoutBill, CancellationToken cancellationToken)
    {
        try
        {
            var products = await _productRepository.TopNew(count, 0, null, cancellationToken);
            if (products.Any(x => x.Prices.Any(p => p.ArticleCode != null)))
                products = await _articleRepository.AddPriceAndExistFromHolooList(products, isWithoutBill, true, cancellationToken);

            products = products.OrderByDescending(x => x.Prices.Any(e => e.Exist > 0)).ToList();

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = products
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpGet]
    public async Task<IActionResult> TopPrice(int count, bool isWithoutBill, CancellationToken cancellationToken)
    {
        try
        {
            var products = await _productRepository.TopPrices(count, 0, null, cancellationToken);

            var productIndexPageViewModel = new List<ProductIndexPageViewModel>();
            productIndexPageViewModel.AddRange(products.Select(product => (ProductIndexPageViewModel)product));

            if (products.Any(x => x.Prices.Any(p => p.ArticleCode != null)))
                productIndexPageViewModel = await _articleRepository.AddPriceAndExistFromHolooList(productIndexPageViewModel, isWithoutBill, true, cancellationToken);
            products = products.OrderByDescending(x => x.Prices.Any(e => e.Exist > 0)).ToList();
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = products
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpGet]
    public async Task<IActionResult> TopDiscounts(int count, bool isWithoutBill, CancellationToken cancellationToken)
    {
        try
        {
            var products = await _priceRepository.TopDiscounts(count, cancellationToken);

            var productIndexPageViewModel = new List<ProductIndexPageViewModel>();
            productIndexPageViewModel.AddRange(products.Select(product => (ProductIndexPageViewModel)product));
            if (products.Any(x => x.Prices.Any(p => p.ArticleCode != null)))
                productIndexPageViewModel = await _articleRepository.AddPriceAndExistFromHolooList(productIndexPageViewModel, isWithoutBill, true, cancellationToken);
            productIndexPageViewModel = productIndexPageViewModel.OrderByDescending(x => x.Prices.Any(e => e.Exist > 0)).ToList();
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = productIndexPageViewModel
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpGet]
    public async Task<IActionResult> TopRelatives(int productId, int count, bool isWithoutBill, CancellationToken cancellationToken)
    {
        try
        {
            var products = await _productRepository.TopRelatives(productId, count, cancellationToken);

            var productIndexPageViewModel = new List<ProductIndexPageViewModel>();
            productIndexPageViewModel.AddRange(products.Select(product => (ProductIndexPageViewModel)product));
            if (products.Any(x => x.Prices.Any(p => p.ArticleCode != null)))
                productIndexPageViewModel = await _articleRepository.AddPriceAndExistFromHolooList(productIndexPageViewModel, isWithoutBill, true, cancellationToken);
            productIndexPageViewModel = productIndexPageViewModel.OrderByDescending(x => x.Prices.Any(e => e.Exist > 0)).ToList();
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = productIndexPageViewModel
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpGet]
    public async Task<IActionResult> TopSells(int count, bool isWithoutBill, CancellationToken cancellationToken)
    {
        try
        {
            var products = await _productRepository.TopSells(count, cancellationToken);

            var productIndexPageViewModel = new List<ProductIndexPageViewModel>();
            productIndexPageViewModel.AddRange(products.Select(product => (ProductIndexPageViewModel)product));
            if (products.Any(x => x.Prices.Any(p => p.ArticleCode != null)))
                productIndexPageViewModel = await _articleRepository.AddPriceAndExistFromHolooList(productIndexPageViewModel, isWithoutBill, true, cancellationToken);
            productIndexPageViewModel = productIndexPageViewModel.OrderByDescending(x => x.Prices.Any(e => e.Exist > 0)).ToList();
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = productIndexPageViewModel
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpGet]
    public async Task<IActionResult> TopStars(int count, bool isWithoutBill, CancellationToken cancellationToken)
    {
        try
        {
            var productIndexPageViewModel = await _productRepository.TopStars(count, 0, null, cancellationToken);

            if (productIndexPageViewModel.Any(x => x.Prices.Any(p => p.ArticleCode != null)))
                productIndexPageViewModel = await _articleRepository.AddPriceAndExistFromHolooList(productIndexPageViewModel, isWithoutBill, true, cancellationToken);
            productIndexPageViewModel = productIndexPageViewModel.OrderByDescending(x => x.Prices.Any(e => e.Exist > 0)).ToList();
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = productIndexPageViewModel
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpGet]
    public IActionResult GetCategoryProductCount(int categoryId, CancellationToken cancellationToken)
    {
        try
        {
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = _productRepository.GetCategoryProductCount(categoryId, cancellationToken)
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpGet]
    public async Task<ActionResult<ProductViewModel>> GetByProductUrl(string productUrl, int userId, bool isWithoutBill, bool? isCheckExist, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productRepository.GetByUrl(productUrl, cancellationToken);
            if (product == null) return Ok(new ApiResult
            {
                Code = ResultCode.NotFound
            });
            if (product.Prices.Any(p => p.ArticleCode != null)) product = await AddPriceAndExistFromHoloo(product, isWithoutBill, isCheckExist, cancellationToken);

            var wish = await _wishListRepository.GetByProductUser(product.Id, userId, cancellationToken);
            var result = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                IsDiscontinued = product.IsDiscontinued,
                IsActive = product.IsActive,
                Url = product.Url,
                HolooCompanyId = product.HolooCompanyId,
                StoreId = product.StoreId,
                Images = product.Images.ToList(),
                TagsId = product.Tags.Select(x => x.Id).ToList(),
                KeywordsId = product.Keywords.Select(x => x.Id).ToList(),
                Prices = product.Prices,
                Supplier = product.Supplier,
                Tags = product.Tags.ToList(),
                Keywords = product.Keywords.ToList(),
                WishListPriceId = wish == null ? null : wish.PriceId
            };

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = result
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }


    [HttpGet]
    public async Task<ActionResult<ProductViewModel>> GetById(int id, bool isColleague,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _productRepository.GetProductByIdWithInclude(id).FirstOrDefaultAsync(cancellationToken);
            if (result == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.NotFound
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = result
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetByIdViewModel(int id, bool isColleague, bool? isCheckExist, bool isWithoutBill,
      CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productRepository.GetProductById(id, cancellationToken);

            if (product == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.NotFound
                });

            if (product.Prices.Any(p => p.ArticleCode != null))
                product = await AddPriceAndExistFromHoloo(product, isWithoutBill, isCheckExist, cancellationToken);

            var productModalViewModel = new ProductModalViewModel
            {
                Alt = product.Images.FirstOrDefault().Alt,
                ImagePath = $"{product.Images!.First().Path}/{product.Images!.First().Name}",
                Brand = product.Brand != null ? product.Brand.Name : "",
                Description = product.Description ?? "",
                Name = product.Name,
                Price = product.Prices.FirstOrDefault().Amount.ToString("###,###,###,###"),
                Url = product.Url,
                Exist = product.Prices.FirstOrDefault().Exist
            };

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = productModalViewModel
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpPost]
    public async Task<IActionResult> ProductsWithIdsForCompare(List<int> productIdList, CancellationToken cancellationToken)
    {
        try
        {
            var resultFormRepository = _productRepository.GetProductListWithAttribute(productIdList);
            var result = await _articleRepository.AddPriceAndExistFromHolooList(resultFormRepository.ToList(), true, false, cancellationToken);
            if (result == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.NotFound
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = result
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetProductsWithCategoriesForCompare(int categoryId, CancellationToken cancellationToken)
    {
        try
        {
            List<int> ProductsIds = _productRepository.GetProductsIdWithCategories(categoryId);
            var resultFormRepository = _productRepository.GetProductListWithAttribute(ProductsIds);
            var result = await _articleRepository.AddPriceAndExistFromHolooList(resultFormRepository.ToList(), true, false, cancellationToken);
            if (result == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.NotFound
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = result
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpPost]
    public async Task<IActionResult> ProductsWithIdsForCart(List<int> productIdList, bool isWithoutBill,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _productRepository.GetProductList(productIdList, cancellationToken);
            result = await _articleRepository.AddPriceAndExistFromHolooList(result, isWithoutBill, true, cancellationToken);
            if (result == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.NotFound
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = result
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Post(ProductViewModel productViewModel, CancellationToken cancellationToken)
    {
        try
        {
            if (productViewModel == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });
            productViewModel.Name = productViewModel.Name.Trim();

            var repetitiveName = await _productRepository.GetByName(productViewModel.Name, cancellationToken);
            if (repetitiveName != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> { "نام محصول تکراری است" }
                });

            var repetitiveUrl = await _productRepository.GetByUrl(productViewModel.Url, cancellationToken);
            if (repetitiveUrl != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> { "آدرس محصول تکراری است" }
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _productRepository.AddWithRelations(productViewModel, cancellationToken)
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<int>> Put(ProductViewModel productViewModel, CancellationToken cancellationToken)
    {
        try
        {
            if (productViewModel == null) return BadRequest();

            var repetitiveName = await _productRepository.GetByName(productViewModel.Name, cancellationToken);
            if (repetitiveName != null && repetitiveName.Id != productViewModel.Id)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> { "نام محصول تکراری است" }
                });
            if (repetitiveName != null) _productRepository.Detach(repetitiveName);

            var repetitiveUrl = await _productRepository.GetByUrl(productViewModel.Url, cancellationToken);
            if (repetitiveUrl != null && repetitiveUrl.Id != productViewModel.Id)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> { "آدرس محصول تکراری است" }
                });
            if (repetitiveUrl != null) _productRepository.Detach(repetitiveUrl);

            var result = await _productRepository.EditWithRelations(productViewModel, cancellationToken);
            if (productViewModel.Prices.Count > 0)
                await _priceRepository.EditAll(productViewModel.Prices, result.Id, cancellationToken);

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = result
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _productRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetMGroup(CancellationToken cancellationToken)
    {
        try
        {
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _mGroupRepository.GetAll(cancellationToken)
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetSGroupByMGroupCode(string mCode, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _sGroupRepository.GetSGroupByMCode(mCode, cancellationToken);
            if (result == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.NotFound
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = result
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllArticleMCodeSCode(string code, CancellationToken cancellationToken)
    {
        try
        {
            //var products = (await _articleRepository.GetAllArticleMCodeSCode(code, cancellationToken))!.
            //    Select(x => new Product()
            //    {
            //        Name = x.A_Name,
            //        Description = x.Other1,
            //        MinInStore = x.A_Min,
            //        IsActive = x.IsActive,
            //        Prices = new List<Price>()
            //        {
            //            new Price()
            //            {
            //                Amount = Convert.ToInt32(x.Sel_Price),
            //                CurrencyId = 1,
            //                MinQuantity = 1,
            //                MaxQuantity = 0,
            //                UnitId =_unitRepository.GetId(x.VahedCode,cancellationToken),
            //                SellNumber=0,
            //                ArticleCode = x.A_Code,
            //                ArticleCodeCustomer = x.A_Code_C,
            //                Exist = x.Exist ?? 0
            //            }
            //        }
            //    });
            var products = await _articleRepository.GetAllArticleMCodeSCode(code, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = products
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> ConvertHolooToSunflower([FromBody] string mCode,
        CancellationToken cancellationToken)
    {
        try
        {
            var mCodeLis = new List<HolooMGroup>();
            if (mCode.Equals(""))
                mCodeLis = (await _mGroupRepository.GetAll(cancellationToken))!.ToList();
            else
                mCodeLis.Add(await _mGroupRepository.GetByCode(mCode, cancellationToken));
            foreach (var mGroup in mCodeLis)
            {
                var parentCategoryCode = await _categoryRepository.GetByName(mGroup.M_groupname, cancellationToken) ??
                                         await _categoryRepository.AddAsync(new Category
                                         {
                                             Name = mGroup.M_groupname,
                                             Path = "/" + mGroup.M_groupname
                                         }, cancellationToken);

                var sGroups = await _sGroupRepository.GetSGroupByMCode(mGroup.M_groupcode, cancellationToken);

                var holooSGroups = sGroups.ToList();
                if (!holooSGroups.Any())
                {
                    if (mCode == "")
                        continue;
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.BadRequest,
                        Messages = new List<string> { "گروه فرعی برای این گروه اصلی در هلو موجود نمی باشد" }
                    });
                }

                var categories = holooSGroups.Select(x => new Category
                {
                    Name = x.S_groupname,
                    ParentId = parentCategoryCode.Id,
                    Path = parentCategoryCode.Path + "/" + x.S_groupname
                }).ToList();

                foreach (var sGroup in holooSGroups)
                {
                    var articles =
                        await _articleRepository.GetAllArticleMCodeSCode(sGroup.M_groupcode + sGroup.S_groupcode,
                            cancellationToken);
                    if (!articles.Any()) continue;
                    var category = await _categoryRepository.AddAsync(new Category
                    {
                        Name = sGroup.S_groupname,
                        ParentId = parentCategoryCode.Id,
                        Path = parentCategoryCode.Path + "/" + sGroup.S_groupname
                    }, cancellationToken);
                    var products = articles.Select(x => new Product
                    {
                        ProductCategories = new List<Category> { category },
                        Name = x.A_Name,
                        Description = x.Other1,
                        MinInStore = x.A_Min,
                        IsActive = x.IsActive,
                        //DiscountId = 1,
                        Url = x.A_Name.Replace(" ", "_"),
                        HolooCompanyId = 1,
                        SupplierId = 1,
                        StoreId = 1,
                        Prices = new List<Price>
                        {
                            new()
                            {
                                DiscountId = 1,
                                Amount = Convert.ToUInt64(x.Sel_Price),
                                CurrencyId = 1,
                                MinQuantity = 1,
                                MaxQuantity = 0,
                                UnitId = _unitRepository.GetId(x.VahedCode, cancellationToken),
                                ArticleCode = x.A_Code,
                                ArticleCodeCustomer = x.A_Code_C,
                                Exist = x.Exist ?? 0,
                                SellNumber = 0
                            }
                        },
                        Images = new List<Image>
                        {
                            new()
                            {
                                Alt = "No Image",
                                Path = "Images/Products",
                                Name = "NoImage.png"
                            }
                        }
                    });
                    try
                    {
                        await _productRepository.AddRangeAsync(products, cancellationToken);
                    }
                    catch (Exception e)
                    {
                        _logger.LogCritical(e, e.Message);
                        return Ok(new ApiResult
                        {
                            Code = ResultCode.Error,
                            Messages = new List<string> { "افزودن اتوماتیک به مشکل برخورد کرد", e.Message }
                        });
                    }
                }

                //var allHolooProducts = (await productRepository.GetAllHolooProducts())!.Select(x => new HolooArticle()
                //{
                //    A_Code = x.ArticleCode,
                //    WebId = x.Id
                //});
                //result = await _articleRepository.SyncHolooWebId(allHolooProducts);
                //if (result == 0)
                //    return BadRequest("بروزرسانی هلو به مشکل برخورد کرد");
            }

            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetTops(string includeProperties, bool isWithoutBill, int? userid, CancellationToken cancellationToken)
    {
        try
        {
            List<ProductIndexPageViewModel> selectedProducts = new List<ProductIndexPageViewModel>();

            string[] parameters = includeProperties.Split(",");
            foreach (var param in parameters)
            {
                List<ProductIndexPageViewModel> products = new List<ProductIndexPageViewModel>();
                var resultCount = param.Split(":");
                int count = System.Convert.ToInt32(resultCount[1]);
                int start = 0;
                int countFilled = 0;
                switch (resultCount[0])
                {
                    case "TopNew":
                        start = 0;
                        countFilled = 0;
                        while (countFilled < count)
                        {
                            products = await _productRepository.TopNew(count, start, resultCount[0],
                                cancellationToken);
                            if (products.Any(x => x.Prices.Any(p => p.ArticleCode != null)))
                            {
                                products = await _articleRepository.AddPriceAndExistFromHolooList(products, isWithoutBill, true,
                                    cancellationToken);
                            }
                            countFilled = selectedProducts.Count(x =>
                                x.TopCategory != null && x.TopCategory.Equals(resultCount[0]));
                            selectedProducts.AddRange(products.Take(count - countFilled).OrderByDescending(x => x.Id));
                            countFilled = selectedProducts.Count(x =>
                                x.TopCategory != null && x.TopCategory.Equals(resultCount[0]));
                            start += count * 2;
                        }
                        break;

                    case "TopPrices":
                        start = 0;
                        countFilled = 0;
                        while (countFilled < count)
                        {
                            products = await _productRepository.TopPrices(count * 2, start, resultCount[0],
                            cancellationToken);
                            if (products.Any(x => x.Prices.Any(p => p.ArticleCode != null)))
                            {
                                products = await _articleRepository.AddPriceAndExistFromHolooList(products, isWithoutBill, true,
                                    cancellationToken);
                            }
                            countFilled = selectedProducts.Count(x =>
                                x.TopCategory != null && x.TopCategory.Equals(resultCount[0]));
                            selectedProducts.AddRange(products.Take(count - countFilled));
                            countFilled = selectedProducts.Count(x =>
                                x.TopCategory != null && x.TopCategory.Equals(resultCount[0]));
                            start += count * 2;
                        }
                        break;

                    case "TopChip":

                        break;
                    case "TopDiscount":

                        break;
                    case "TopRelative":

                        break;
                    case "TopSells":

                        break;

                    case "TopStars":
                        start = 0;
                        countFilled = 0;
                        while (countFilled < count)
                        {
                            products = await _productRepository.TopStars(count * 2, start, resultCount[0],
                                cancellationToken);
                            if (products.Any(x => x.Prices.Any(p => p.ArticleCode != null)))
                            {
                                products = await _articleRepository.AddPriceAndExistFromHolooList(products, isWithoutBill, true,
                                    cancellationToken);
                            }

                            countFilled = selectedProducts.Count(x =>
                                x.TopCategory != null && x.TopCategory.Equals(resultCount[0]));
                            selectedProducts.AddRange(products.Take(count - countFilled));
                            countFilled = selectedProducts.Count(x =>
                                x.TopCategory != null && x.TopCategory.Equals(resultCount[0]));
                            start += count * 2;
                        }

                        break;
                }
            }
            if (userid != 0)
            {
                foreach (var item in selectedProducts)
                {
                    var price = item.Prices.OrderBy(p => p.Amount).FirstOrDefault(x => !x.IsColleague);
                    var wishlists = await _wishListRepository.Where(x => x.UserId == userid && x.PriceId == price.Id, cancellationToken);
                    item.FirstPriceWichlist = wishlists.Any();
                }
            }

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = selectedProducts
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
            { Code = ResultCode.DatabaseError, Messages = new List<string> { "اشکال در سمت سرور" } });
        }
    }

}