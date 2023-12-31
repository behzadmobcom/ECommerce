﻿using ECommerce.API.Interface;
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
    private readonly IHolooABailRepository _aBailRepository;
    private readonly IUnitRepository _unitRepository;
    private readonly ITagRepository _tagRepository;

    public ProductsController(IProductRepository productRepository, IHolooArticleRepository articleRepository,
        IHolooMGroupRepository mGroupRepository, IHolooSGroupRepository sGroupRepository,
        ICategoryRepository categoryRepository, IPriceRepository priceRepository, IUnitRepository unitRepository,
        ILogger<ProductsController> logger, IHolooABailRepository aBailRepository, ITagRepository tagRepository)
    {
        _productRepository = productRepository;
        _articleRepository = articleRepository;
        _mGroupRepository = mGroupRepository;
        _sGroupRepository = sGroupRepository;
        _categoryRepository = categoryRepository;
        _priceRepository = priceRepository;
        _unitRepository = unitRepository;
        _logger = logger;
        _aBailRepository = aBailRepository;
        _tagRepository = tagRepository;
    }

    private async Task<List<T>> AddPriceAndExistFromHolooList<T>(
        IList<T> products, bool isWithoutBill, bool? isExist, CancellationToken cancellationToken) where T : BaseProductPageViewModel
    {
        var aBails = await _aBailRepository.GetAll(cancellationToken);
        var prices = products
            .Where(x => x.Prices.Any(p => p.ArticleCode != null))
            .Select(p => p.Prices)
            .ToList();
        var aCodeCs = new List<string>();
        foreach (var price in prices)
        {
            Parallel.ForEach(price, aCode =>
            //foreach (var aCode in price)
            {
                aCodeCs.Add(aCode.ArticleCodeCustomer);
            });
        }
        var holooArticle = await _articleRepository.GetHolooArticles(aCodeCs, cancellationToken);

        products = products.Where(x => x.Prices.Any(p => p.ArticleCode != null)).ToList();
        List<T> newProducts = new();
        Parallel.ForEach(products, product =>
        //foreach (var product in products)
        {
            List<Price> tempPriceList = new();
            //foreach (var productPrices in product.Prices)
            Parallel.ForEach(product.Prices, productPrices =>
            {
                if (productPrices.SellNumber != null && productPrices.SellNumber != Price.HolooSellNumber.خالی)
                {
                    var article = holooArticle.Where(x => x.A_Code_C == productPrices.ArticleCodeCustomer).ToList();
                    decimal articlePrice = 0;
                    if (article.Count > 0)
                    {
                        try
                        {
                            switch (productPrices.SellNumber)
                            {
                                case Price.HolooSellNumber.Sel_Price:
                                    articlePrice = Convert.ToDecimal(article.FirstOrDefault().Sel_Price);
                                    break;
                                case Price.HolooSellNumber.Sel_Price2:
                                    articlePrice = Convert.ToDecimal(article.FirstOrDefault().Sel_Price2);
                                    break;
                                case Price.HolooSellNumber.Sel_Price3:
                                    articlePrice = Convert.ToDecimal(article.FirstOrDefault().Sel_Price3);
                                    break;
                                case Price.HolooSellNumber.Sel_Price4:
                                    articlePrice = Convert.ToDecimal(article.FirstOrDefault().Sel_Price4);
                                    break;
                                case Price.HolooSellNumber.Sel_Price5:
                                    articlePrice = Convert.ToDecimal(article.FirstOrDefault().Sel_Price5);
                                    break;
                                case Price.HolooSellNumber.Sel_Price6:
                                    articlePrice = Convert.ToDecimal(article.FirstOrDefault().Sel_Price6);
                                    break;
                                case Price.HolooSellNumber.Sel_Price7:
                                    articlePrice = Convert.ToDecimal(article.FirstOrDefault().Sel_Price7);
                                    break;
                                case Price.HolooSellNumber.Sel_Price8:
                                    articlePrice = Convert.ToDecimal(article.FirstOrDefault().Sel_Price8);
                                    break;
                                case Price.HolooSellNumber.Sel_Price9:
                                    articlePrice = Convert.ToDecimal(article.FirstOrDefault().Sel_Price9);
                                    break;
                                case Price.HolooSellNumber.Sel_Price10:
                                    articlePrice = Convert.ToDecimal(article.FirstOrDefault().Sel_Price10);
                                    break;
                            }
                        }
                        catch (Exception e)
                        {
                            _logger.LogCritical(e,
                                $"Product Prices ID : {productPrices.Id}, Article Code : {productPrices.ArticleCode}, Article Code Customer : {productPrices.ArticleCodeCustomer}");
                        }

                        if (articlePrice < 10)
                        {
                            //product.Prices.Remove(productPrices);
                            tempPriceList.Add(productPrices);
                            //continue;
                        }
                        else
                        {
                            productPrices.Amount = articlePrice / 10;
                            double soldExist = 0;
                            if (!isWithoutBill)
                            {
                                var sold = aBails.FirstOrDefault(x => x.A_Code == productPrices.ArticleCode);
                                soldExist = sold == null ? 0 : sold.First_Article;
                            }

                            productPrices.Exist = (double)article.Sum(x => x.Exist) - soldExist;

                            if (isExist == true && productPrices.Exist == 0)
                            {
                                //product.Prices.Remove(productPrices);
                                tempPriceList.Add(productPrices);
                            }
                        }
                    }
                }

            });

            product.Prices.RemoveAll(x => tempPriceList.Contains(x));
            if (product.Prices.Count != 0) newProducts.Add(product);


        });

        return newProducts;
    }

    private async Task<Product> AddPriceAndExistFromHoloo(Product product, bool isWithoutBill, bool? isExist, CancellationToken cancellationToken)
    {
        var products = await AddPriceAndExistFromHolooList(
            new List<ProductIndexPageViewModel>{product},
            isWithoutBill,
            isExist,
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
                        productIndexPageViewModel.AddRange(await productQuery
                            .Where(x => x.Name.Contains(search[1]) || x.Description.Contains(search[1]))
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
                productIndexPageViewModel = await AddPriceAndExistFromHolooList(productIndexPageViewModel, productListFilteredViewModel.isWithoutBill, productListFilteredViewModel.IsExist, cancellationToken);
            }

            if (productListFilteredViewModel.EndPrice > 0)
            {
                productIndexPageViewModel = productIndexPageViewModel.Where(x => x.Prices.Max(p => p.Amount) >= productListFilteredViewModel.StartPrice && x.Prices.Max(p => p.Amount) <= productListFilteredViewModel.EndPrice).ToList();
            }

            if (productListFilteredViewModel.IsExist != null)
            {
                var s = productIndexPageViewModel.Where(x => x.Prices.Any(e => e.Exist == 0)).ToList();
            }

            switch (productListFilteredViewModel.ProductSort)
            {
                case ProductSort.New:
                    productIndexPageViewModel = productIndexPageViewModel.OrderByDescending(x => x.Id).ToList();
                    break;
                case ProductSort.Star:
                    productIndexPageViewModel = productIndexPageViewModel.OrderByDescending(x => x.Stars).ToList();
                    break;
                case ProductSort.HighToLowPrice:
                    productIndexPageViewModel = productIndexPageViewModel.OrderByDescending(x => x.Prices.Max(p => p.Amount)).ToList();
                    break;
                case ProductSort.LowToHighPrice:
                    productIndexPageViewModel = productIndexPageViewModel.OrderBy(x => x.Prices.Min(p => p.Amount)).ToList();
                    break;
                case ProductSort.Bestsellers:
                    productIndexPageViewModel = productIndexPageViewModel.OrderBy(x => x.Prices.Max(p => p.Amount)).ToList();
                    break;
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
    public async Task<IActionResult> GetAllProducts(bool isWithoutBill, bool? isExist, CancellationToken cancellationToken)
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
                productIndexPageViewModel = await AddPriceAndExistFromHolooList(productIndexPageViewModel, isWithoutBill, isExist, cancellationToken);
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
            var products = await _productRepository.TopNew(count, cancellationToken);
            if (products.Any(x => x.Prices.Any(p => p.ArticleCode != null)))
                products = await AddPriceAndExistFromHolooList(products, isWithoutBill, true, cancellationToken);

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
            var products = await _productRepository.TopPrices(count, cancellationToken);

            var productIndexPageViewModel = new List<ProductIndexPageViewModel>();
            productIndexPageViewModel.AddRange(products.Select(product => (ProductIndexPageViewModel)product));

            if (products.Any(x => x.Prices.Any(p => p.ArticleCode != null)))
                productIndexPageViewModel = await AddPriceAndExistFromHolooList(productIndexPageViewModel, isWithoutBill, true, cancellationToken);
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
                productIndexPageViewModel = await AddPriceAndExistFromHolooList(productIndexPageViewModel, isWithoutBill, true, cancellationToken);
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
                productIndexPageViewModel = await AddPriceAndExistFromHolooList(productIndexPageViewModel, isWithoutBill, true, cancellationToken);
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
                productIndexPageViewModel = await AddPriceAndExistFromHolooList(productIndexPageViewModel, isWithoutBill, true, cancellationToken);
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
            var productIndexPageViewModel = await _productRepository.TopStars(count, cancellationToken);

            if (productIndexPageViewModel.Any(x => x.Prices.Any(p => p.ArticleCode != null)))
                productIndexPageViewModel = await AddPriceAndExistFromHolooList(productIndexPageViewModel, isWithoutBill, true, cancellationToken);
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
    public async Task<ActionResult<Product>> GetByProductUrl(string productUrl, bool isWithoutBill, bool? isExist, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _productRepository.GetByUrl(productUrl, cancellationToken);
            if (result == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.NotFound
                });

            if (result.Prices.Any(p => p.ArticleCode != null)) result = await AddPriceAndExistFromHoloo(result, isWithoutBill,isExist, cancellationToken);

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
    public async Task<IActionResult> GetByIdViewModel(int id, bool isColleague, bool? isExist, bool isWithoutBill,
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
                product = await AddPriceAndExistFromHoloo(product, isWithoutBill, isExist,cancellationToken);

            var productModalViewModel = new ProductModalViewModel
            {
                Alt = product.Images.FirstOrDefault().Alt,
                ImagePath = $"{product.Images!.First().Path}/{product.Images!.First().Name}",
                Brand = product.Brand.Name,
                Description = product.Description,
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
    public async Task<IActionResult> ProductsWithIdsForCompare(List<int?> productIdList,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = _productRepository.GetProductListWithAttribute(productIdList);
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
            result = await AddPriceAndExistFromHolooList(result, isWithoutBill, true, cancellationToken);
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
    public async Task<IActionResult> GetTops(string includeProperties, bool isWithoutBill, CancellationToken cancellationToken)
    {
        try
        {
            List<ProductIndexPageViewModel> selectedProducts = new List<ProductIndexPageViewModel>();
            var allProducts = await _productRepository.GetAllWithInclude(cancellationToken)
                                 .Select(p => new ProductIndexPageViewModel
                                 {
                                     Prices = p.Prices,
                                     Alt = p.Images!.First().Alt,
                                     Brand = p.Brand!.Name,
                                     Name = p.Name,
                                     Description = p.Description,
                                     Id = p.Id,
                                     ImagePath = $"{p.Images!.First().Path}/{p.Images!.First().Name}",
                                     Stars = p.ProductUserRanks.Count > 0 ? p.ProductUserRanks.Sum(x => x.Stars) / p.ProductUserRanks.Count : 0,
                                     Url = p.Url,
                                 }).ToListAsync(cancellationToken);

            if (allProducts.Any(x => x.Prices.Any(p => p.ArticleCode != null)))
                allProducts = await AddPriceAndExistFromHolooList(allProducts, isWithoutBill, true, cancellationToken);
            allProducts = allProducts.OrderByDescending(x => x.Prices.Any(e => e.Exist > 0)).ToList();

            string[] parameters = includeProperties.Split(",");
            foreach (var param in parameters)
            {
                List<ProductIndexPageViewModel> products = new List<ProductIndexPageViewModel>();
                var resultCount = param.Split(":");
                int _count = System.Convert.ToInt32(resultCount[1]);
                switch (resultCount[0])
                {
                    case "TopNew":
                        products = allProducts.OrderByDescending(x => x.Id).Take(_count)
                       .Select(p => new ProductIndexPageViewModel
                       {
                           Prices = p.Prices,
                           Alt = p.Alt,
                           Brand = p.Brand,
                           Name = p.Name,
                           Description = p.Description,
                           Id = p.Id,
                           ImagePath = p.ImagePath,
                           Stars = p.Stars,
                           Url = p.Url,
                           TopCategory = resultCount[0]
                       }).ToList();
                        break;

                    case "TopPrices":
                        products = allProducts
                        .Select(p => new ProductIndexPageViewModel
                        {
                            Prices = p.Prices,
                            Alt = p.Alt,
                            Brand = p.Brand,
                            Name = p.Name,
                            Description = p.Description,
                            Id = p.Id,
                            ImagePath = p.ImagePath,
                            Stars = p.Stars,
                            Url = p.Url,
                            MaxPrice = p.Prices.Sum(x => x.Exist) > 0 ? p.Prices.Select(x => x.Amount).Max() : 0,
                            TopCategory = resultCount[0]
                        })
                        .OrderByDescending(o => o.MaxPrice)
                        .Take(_count)
                        .ToList();
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
                        products = allProducts.OrderByDescending(x => x.Stars).Take(_count)
                        .Select(p => new ProductIndexPageViewModel
                        {
                            Prices = p.Prices,
                            Alt = p.Alt,
                            Brand = p.Brand,
                            Name = p.Name,
                            Description = p.Description,
                            Id = p.Id,
                            ImagePath = p.ImagePath,
                            Stars = p.Stars,
                            Url = p.Url,
                            TopCategory = resultCount[0]
                        }).ToList();
                        break;

                    default: break;
                }
                foreach (var product in products) selectedProducts.Add(product);
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