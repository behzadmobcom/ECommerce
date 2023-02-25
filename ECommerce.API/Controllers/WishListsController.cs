using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Client,Admin,SuperAdmin")]
public class WishListsController : ControllerBase
{
    private readonly ILogger<WishListsController> _logger;
    private readonly IWishListRepository _wishListRepository;

    public WishListsController(IWishListRepository wishListRepository, ILogger<WishListsController> logger)
    {
        _wishListRepository = wishListRepository;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _wishListRepository.GetByIdWithInclude(id, cancellationToken);

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = result
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult {Code = ResultCode.DatabaseError});
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(WishList wishList, CancellationToken cancellationToken)
    {
        try
        {
            if (wishList == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });

            var repetitiveWishList =
                await _wishListRepository.GetByProductUser(wishList.ProductId, wishList.UserId, cancellationToken);
            if (repetitiveWishList != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {repetitiveWishList.Id.ToString()}
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _wishListRepository.AddAsync(wishList, cancellationToken)
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult {Code = ResultCode.DatabaseError});
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _wishListRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult {Code = ResultCode.DatabaseError});
        }
    }
}