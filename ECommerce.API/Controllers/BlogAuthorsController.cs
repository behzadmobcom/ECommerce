using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BlogAuthorsController : ControllerBase
{
    private readonly IBlogAuthorRepository _blogAuthorRepository;
    private readonly ILogger<BlogAuthorsController> _logger;

    public BlogAuthorsController(IBlogAuthorRepository brandRepository, ILogger<BlogAuthorsController> logger)
    {
        _blogAuthorRepository = brandRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        
            if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
            var entity = await _blogAuthorRepository.Search(paginationParameters, cancellationToken);
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

    [HttpGet]
    public async Task<ActionResult<BlogAuthor>> GetAll(CancellationToken cancellationToken)
    {
        
            var result = await _blogAuthorRepository.GetAll(cancellationToken);
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

    [HttpGet]
    public async Task<ActionResult<BlogAuthor>> GetById(int id, CancellationToken cancellationToken)
    {
        
            var result = await _blogAuthorRepository.GetByIdAsync(cancellationToken, id);
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

    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Post(BlogAuthor blogAuthor, CancellationToken cancellationToken)
    {
        
            if (blogAuthor == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });
            blogAuthor.Name = blogAuthor.Name.Trim();

            var repetitiveAuthor = await _blogAuthorRepository.GetByName(blogAuthor.Name, cancellationToken);
            if (repetitiveAuthor != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"نام نویسنده تکراری است"}
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _blogAuthorRepository.AddAsync(blogAuthor, cancellationToken)
            });
       
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(BlogAuthor blogAuthor, CancellationToken cancellationToken)
    {
        
            await _blogAuthorRepository.UpdateAsync(blogAuthor, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
       
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        
            await _blogAuthorRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
       
    }
}