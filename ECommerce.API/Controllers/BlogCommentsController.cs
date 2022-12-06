using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BlogCommentsController : ControllerBase
{
    private readonly IBlogCommentRepository _blogCommentRepository;
    private readonly ILogger<BlogCommentsController> _logger;

    public BlogCommentsController(IBlogCommentRepository blogCommentRepository, ILogger<BlogCommentsController> logger)
    {
        _blogCommentRepository = blogCommentRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
            var entity = await _blogCommentRepository.Search(paginationParameters, cancellationToken);
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
    public async Task<ActionResult<BlogComment>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _blogCommentRepository.GetByIdAsync(cancellationToken, id);
            if (result.AnswerId != null) result.Answer = await _blogCommentRepository.GetByIdAsync(cancellationToken, result.AnswerId);
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
    public async Task<IActionResult> Post(BlogComment blogComment, CancellationToken cancellationToken)
    {
        try
        {
            if (blogComment == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });

            blogComment.IsAccepted = false;
            blogComment.IsRead = false;
            blogComment.IsAnswered = false;
            blogComment.DateTime = DateTime.Now;

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _blogCommentRepository.AddAsync(blogComment, cancellationToken)
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
    public async Task<ActionResult<bool>> Put(BlogComment blogComment, CancellationToken cancellationToken)
    {
        try
        {
            BlogComment? _commentAnswer;
            if (blogComment.AnswerId != null)
            {
                _commentAnswer = await _blogCommentRepository.GetByIdAsync(cancellationToken, blogComment.AnswerId);
                _commentAnswer.Text = blogComment.Answer.Text;
                _commentAnswer.DateTime = DateTime.Now;
                await _blogCommentRepository.UpdateAsync(_commentAnswer, cancellationToken);
            }
            else
            {
                if (blogComment.Answer?.Text != null)
                {
                    blogComment.Answer.Name = "پاسخ ادمین";
                    blogComment.Answer.IsAccepted = false;
                    blogComment.Answer.IsRead = false;
                    blogComment.Answer.IsAnswered = false;
                    blogComment.Answer.DateTime = DateTime.Now;
                    _commentAnswer = await _blogCommentRepository.AddAsync(blogComment.Answer, cancellationToken);
                    if (_commentAnswer != null) { blogComment.Answer = _commentAnswer; blogComment.AnswerId = _commentAnswer.Id; }
                }
            }

            await _blogCommentRepository.UpdateAsync(blogComment, cancellationToken);
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
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _blogCommentRepository.DeleteAsync(id, cancellationToken);
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
    public async Task<IActionResult> GetAllAccesptedComments(int blogId, CancellationToken cancellationToken)
    {
        var result = _blogCommentRepository.GetAllAccesptedComments(blogId, cancellationToken);
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