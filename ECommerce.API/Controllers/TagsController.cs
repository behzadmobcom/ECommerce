using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly ILogger<TagsController> _logger;
    private readonly ITagRepository _tagRepository;

    public TagsController(ITagRepository tagRepository, ILogger<TagsController> logger)
    {
        _tagRepository = tagRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        
            if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
            var entity = await _tagRepository.Search(paginationParameters, cancellationToken);
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
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _tagRepository.GetAll(cancellationToken)
            });
    }

    [HttpGet]
    public async Task<IActionResult> GetByProductId(int id, CancellationToken cancellationToken)
    {
        
            var tagList = await _tagRepository.GetByProductId(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = tagList
            });
    }

    [HttpGet]
    public async Task<ActionResult<Tag>> GetById(int id, CancellationToken cancellationToken)
    {
        
            var result = await _tagRepository.GetByIdAsync(cancellationToken, id);
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
    public async Task<IActionResult> Post(Tag tag, CancellationToken cancellationToken)
    {
        
            if (tag == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest
                });
            tag.TagText = tag.TagText.Trim();

            var repetitiveTag = await _tagRepository.GetByTagText(tag.TagText, cancellationToken);
            if (repetitiveTag != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"تگ تکراری است"}
                });


            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _tagRepository.AddAsync(tag, cancellationToken)
            });
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(Tag tag, CancellationToken cancellationToken)
    {
        
            var repetitive = await _tagRepository.GetByTagText(tag.TagText, cancellationToken);
            if (repetitive != null && repetitive.Id != tag.Id)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"تگ تکراری است"}
                });
            if (repetitive != null) _tagRepository.Detach(repetitive);
            await _tagRepository.UpdateAsync(tag, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        
            await _tagRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductTags(CancellationToken cancellationToken)
    {
        
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _tagRepository.GetAllProductTags(cancellationToken)
            });
       
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBlogTags(CancellationToken cancellationToken)
    {
        
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _tagRepository.GetAllBlogTags(cancellationToken)
            });
       
    }
}