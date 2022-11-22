using ECommerce.API.Interface;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BlogsController : ControllerBase
{
    private readonly IBlogRepository _blogRepository;
    private readonly ILogger<BlogsController> _logger;

    public BlogsController(IBlogRepository blogRepository, ILogger<BlogsController> logger)
    {
        _blogRepository = blogRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";     
            var entity = await _blogRepository.Search(paginationParameters, cancellationToken);
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
    public async Task<IActionResult> GetByTagText([FromQuery] PaginationParameters paginationParameters,
    CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(paginationParameters.TagText)) paginationParameters.TagText = "";
            var entity = await _blogRepository.GetByTagText(paginationParameters, cancellationToken);
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
            return Ok(new ApiResult { Code = ResultCode.DatabaseError });
        }
    }

    [HttpGet]
    public async Task<ActionResult<BlogViewModel>> GetById(int id, bool isColleague,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _blogRepository.GetBlogByIdWithInclude(id).FirstOrDefaultAsync(cancellationToken);
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
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Post(BlogViewModel blogViewModel, CancellationToken cancellationToken)
    {
        try
        {
            if (blogViewModel == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });
            blogViewModel.Title = blogViewModel.Title.Trim();

            var repetitiveTitle = await _blogRepository.GetByTitle(blogViewModel.Title, cancellationToken);
            if (repetitiveTitle != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"عنوان مقاله تکراری است"}
                });
            var repetitiveUrl = await _blogRepository.GetByUrl(blogViewModel.Url, cancellationToken);
            if (repetitiveUrl != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"آدرس مقاله تکراری است"}
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _blogRepository.AddWithRelations(blogViewModel, cancellationToken)
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
    public async Task<ActionResult<int>> Put(BlogViewModel blogViewModel, CancellationToken cancellationToken)
    {
        try
        {
            if (blogViewModel == null) return BadRequest();


            var repetitiveTitle = await _blogRepository.GetByTitle(blogViewModel.Title, cancellationToken);
            if (repetitiveTitle != null && repetitiveTitle.Id != blogViewModel.Id)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"عنوان مقاله تکراری است"}
                });
            if (repetitiveTitle != null) _blogRepository.Detach(repetitiveTitle);
            var repetitiveUrl = await _blogRepository.GetByUrl(blogViewModel.Url, cancellationToken);
            if (repetitiveUrl != null && repetitiveUrl.Id != blogViewModel.Id)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"آدرس مقاله تکراری است"}
                });
            if (repetitiveUrl != null) _blogRepository.Detach(repetitiveUrl);
            await _blogRepository.EditWithRelations(blogViewModel, cancellationToken);
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
            await _blogRepository.DeleteAsync(id, cancellationToken);
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
    public async Task<IActionResult> GetByCategory(int id, CancellationToken cancellationToken)
    {
        try
        {
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _blogRepository.GetWithInclude(id, cancellationToken)
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult {Code = ResultCode.DatabaseError});
        }
    }

    [HttpGet]
    public async Task<ActionResult<BlogViewModel>> GetByUrl(string blogUrl, bool isColleague,
     CancellationToken cancellationToken)
    {
        try
        {
            var result = await _blogRepository.GetBlogByUrlWithInclude(blogUrl).FirstOrDefaultAsync(cancellationToken);
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
            return Ok(new ApiResult { Code = ResultCode.DatabaseError });
        }
    }
}