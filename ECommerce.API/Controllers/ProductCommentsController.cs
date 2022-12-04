using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProductCommentsController : ControllerBase
{
    private readonly ILogger<ProductCommentsController> _logger;
    private readonly IProductCommentRepository _productCommentRepository;

    public ProductCommentsController(IProductCommentRepository productCommentRepository, IProductRepository productRepository,
        ILogger<ProductCommentsController> logger)
    {
        _productCommentRepository = productCommentRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
            var entity = await _productCommentRepository.Search(paginationParameters, cancellationToken);
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
            return Ok(new ApiResult {Code = ResultCode.DatabaseError});
        }
    }

    [HttpGet]
    public async Task<ActionResult<ProductComment>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _productCommentRepository.GetByIdAsync(cancellationToken, id);
            if (result.AnswerId!=null) result.Answer = await _productCommentRepository.GetByIdAsync(cancellationToken, result.AnswerId);
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
            return Ok(new ApiResult {Code = ResultCode.DatabaseError});
        }
    }

    [HttpPost]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Post(ProductComment productComment, CancellationToken cancellationToken)
    {
        try
        {
            if (productComment == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });

            productComment.IsAccepted = false;
            productComment.IsRead = false;
            productComment.IsAnswered=false;
            productComment.DateTime = DateTime.Now;

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _productCommentRepository.AddAsync(productComment, cancellationToken)
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult {Code = ResultCode.DatabaseError});
        }
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(ProductComment productComment, CancellationToken cancellationToken)
    {
        try
        {
            
            ProductComment? _commentAnswer;
            if (productComment.AnswerId != null)
            {
                _commentAnswer = await _productCommentRepository.GetByIdAsync(cancellationToken, productComment.AnswerId);            
                _commentAnswer.Text = productComment.Answer.Text;
                _commentAnswer.DateTime = DateTime.Now;
                await _productCommentRepository.UpdateAsync(_commentAnswer, cancellationToken);                 
            }
            else
            {
                if (productComment.Answer?.Text != null)
                {
                    productComment.Answer.Name = "پاسخ ادمین";
                    productComment.Answer.IsAccepted = false;
                    productComment.Answer.IsRead = false;
                    productComment.Answer.IsAnswered = false;
                    productComment.Answer.DateTime = DateTime.Now;
                    _commentAnswer = await _productCommentRepository.AddAsync(productComment.Answer, cancellationToken);
                    if (_commentAnswer != null) { productComment.Answer = _commentAnswer; productComment.AnswerId = _commentAnswer.Id; }
                }

            }     
           
                await _productCommentRepository.UpdateAsync(productComment, cancellationToken);
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

    [HttpDelete]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _productCommentRepository.DeleteAsync(id, cancellationToken);
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


    [HttpGet]
    public async Task<IActionResult> GetAllAccesptedComments(int productId, CancellationToken cancellationToken)
    {
        var result = _productCommentRepository.GetAllAccesptedComments(productId,cancellationToken);
        try
        {
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await result.ToListAsync()
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult { Code = ResultCode.DatabaseError });
        }
    }

}