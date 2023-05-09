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
        try
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
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult {Code = ResultCode.DatabaseError});
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _tagRepository.GetAll(cancellationToken)
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult {Code = ResultCode.DatabaseError});
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetByProductId(int id, CancellationToken cancellationToken)
    {
        try
        {
            var tagList = await _tagRepository.GetByProductId(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = tagList
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult {Code = ResultCode.DatabaseError});
        }
    }

    [HttpGet]
    public async Task<ActionResult<Tag>> GetById(int id, CancellationToken cancellationToken)
    {
        try
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
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult {Code = ResultCode.DatabaseError});
        }
    }

    [HttpGet]
    public async Task<ActionResult<Tag>> GetByTagText(string tagText, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _tagRepository.GetByTagText(tagText, cancellationToken);
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

    [HttpGet]
    public async Task<ActionResult<Tag>> GetByTagNames(List<string> tagText, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _tagRepository.GetByTagNames( tagText, cancellationToken);
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

    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Post(Tag tag, CancellationToken cancellationToken)
    {
        try
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
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult {Code = ResultCode.DatabaseError});
        }
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(Tag tag, CancellationToken cancellationToken)
    {
        try
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
            await _tagRepository.DeleteAsync(id, cancellationToken);
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
    public async Task<IActionResult> GetAllProductTags(CancellationToken cancellationToken)
    {
        try
        {
            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _tagRepository.GetAllProductTags(cancellationToken)
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult { Code = ResultCode.DatabaseError });
        }
    }
}