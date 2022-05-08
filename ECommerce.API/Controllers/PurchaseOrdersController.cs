using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Interface;
using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IPurchaseOrderDetailRepository _purchaseOrderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<PurchaseOrdersController> _logger;

        public PurchaseOrdersController(IPurchaseOrderRepository discountRepository, IPurchaseOrderDetailRepository purchaseOrderDetailRepository, IProductRepository productRepository, ILogger<PurchaseOrdersController> logger)
        {
            _purchaseOrderRepository = discountRepository;
            _purchaseOrderDetailRepository = purchaseOrderDetailRepository;
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Client,Admin,SuperAdmin")]
        public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters , CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
                var entity = await _purchaseOrderRepository.Search(paginationParameters, cancellationToken);
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
                 _logger.LogCritical(e, e.Message); return Ok(new ApiResult {Code = ResultCode.DatabaseError});
            }
        }

        [HttpGet]
        [Authorize(Roles = "Client,Admin,SuperAdmin")]
        public async Task<ActionResult<PurchaseOrder>> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _purchaseOrderRepository.GetByIdAsync(cancellationToken,id);
                if (result == null)
                {
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.NotFound
                    });
                }

                return Ok(new ApiResult
                {
                    Code = ResultCode.Success,
                    ReturnData = result
                });
            }
            catch (Exception e)
            {
                 _logger.LogCritical(e, e.Message); return Ok(new ApiResult {Code = ResultCode.DatabaseError});
            }
        }

        [HttpGet]
        [Authorize(Roles = "Client,Admin,SuperAdmin")]
        public async Task<ActionResult<PurchaseOrderViewModel>> UserCart(int userId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _purchaseOrderRepository.GetProductListByUserId(userId, cancellationToken);
                if (result == null)
                {

                    return Ok(new ApiResult
                    {
                        Code = ResultCode.Success,
                        ReturnData = new List<PurchaseOrderViewModel>()
                    });
                }

                return Ok(new ApiResult
                {
                    Code = ResultCode.Success,
                    ReturnData = result
                });
            }
            catch (Exception e)
            {
                 _logger.LogCritical(e, e.Message); return Ok(new ApiResult {Code = ResultCode.DatabaseError});
            }
        }

        [HttpPost]
        [Authorize(Roles = "Client,Admin,SuperAdmin")]
        public async Task<IActionResult> Post(PurchaseOrderViewModel purchaseOrderViewModel, CancellationToken cancellationToken)
        {
            try
            {
                if (purchaseOrderViewModel == null)
                {
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.BadRequest
                    });
                }
                purchaseOrderViewModel.Name = purchaseOrderViewModel.Name.Trim();
                var product =await _productRepository.GetProductByIdWithInclude(purchaseOrderViewModel.ProductId, cancellationToken);


                var colleaguePrice = product.Prices.Where(x => x.IsColleague == purchaseOrderViewModel.IsColleague).ToList();
                var minPrice = colleaguePrice.Any()
                    ? colleaguePrice.Where(x => x.MinQuantity <= purchaseOrderViewModel.Quantity).ToList()
                    : product.Prices.Where(x => x.MinQuantity <= purchaseOrderViewModel.Quantity).ToList();
                var maxPrice = minPrice.Any() ?minPrice.FirstOrDefault(x => x.MaxQuantity >= purchaseOrderViewModel.Quantity) :
                    product.Prices.FirstOrDefault(x => x.MaxQuantity >= purchaseOrderViewModel.Quantity);

                var price = maxPrice ?? product.Prices.FirstOrDefault();

                var unitPrice = price?.Amount ?? 0;
                var sumPrice = unitPrice * purchaseOrderViewModel.Quantity;
                var repetitivePurchaseOrder = await _purchaseOrderRepository.GetByUser(purchaseOrderViewModel.UserId, cancellationToken);
                if (repetitivePurchaseOrder != null)
                {
                    var repetitiveDetail =
                        repetitivePurchaseOrder.PurchaseOrderDetails.FirstOrDefault(x => x.ProductId == purchaseOrderViewModel.ProductId);
                    if (repetitiveDetail != null)
                    {
                        repetitiveDetail.Quantity+= purchaseOrderViewModel.Quantity;
                        repetitiveDetail.SumPrice = repetitiveDetail.Quantity * unitPrice;
                       await _purchaseOrderDetailRepository.UpdateAsync(repetitiveDetail,cancellationToken);
                    }
                    else
                    {
                        await _purchaseOrderDetailRepository.AddAsync(new PurchaseOrderDetail
                        {
                            PurchaseOrderId = repetitivePurchaseOrder.Id,
                            Name = product.Name,
                            UnitPrice = unitPrice,
                            ProductId = product.Id,
                            Quantity = purchaseOrderViewModel.Quantity,
                            SumPrice = sumPrice
                        },cancellationToken);
                        repetitivePurchaseOrder.Amount += sumPrice;
                        await _purchaseOrderRepository.UpdateAsync(repetitivePurchaseOrder,cancellationToken);
                    }
                    return Ok("به سبد خرید اضافه شد");
                }

                var result = await _purchaseOrderRepository.AddAsync(new PurchaseOrder
                {
                    Amount = sumPrice,
                    Status = 0,
                    UserId = purchaseOrderViewModel.UserId 
                },cancellationToken);
                await _purchaseOrderDetailRepository.AddAsync(new PurchaseOrderDetail
                {
                    PurchaseOrderId = result.Id,
                    Name = product.Name,
                    UnitPrice = unitPrice,
                    ProductId = product.Id,
                    Quantity = purchaseOrderViewModel.Quantity,
                    SumPrice = sumPrice
                },cancellationToken);
                return Ok("به سبد خرید اضافه شد");
            }
            catch (Exception e)
            {
                 _logger.LogCritical(e, e.Message); return Ok(new ApiResult {Code = ResultCode.DatabaseError});
            }
        }

        [HttpPut]
        [Authorize(Roles = "Client,Admin,SuperAdmin")]
        public async Task<ActionResult<bool>> Put(PurchaseOrder purchaseOrder, CancellationToken cancellationToken)
        {
            try
            {
                await _purchaseOrderRepository.UpdateAsync(purchaseOrder, cancellationToken);
                return Ok(new ApiResult
                {
                    Code = ResultCode.Success
                });
            }
            catch (Exception e)
            {
                 _logger.LogCritical(e, e.Message); return Ok(new ApiResult {Code = ResultCode.DatabaseError});
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Client,Admin,SuperAdmin")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _purchaseOrderRepository.DeleteAsync(id, cancellationToken);
                return Ok(new ApiResult
                {
                    Code = ResultCode.Success
                });
            }
            catch (Exception e)
            {
                 _logger.LogCritical(e, e.Message); return Ok(new ApiResult {Code = ResultCode.DatabaseError});
            }
        }
    }
}
