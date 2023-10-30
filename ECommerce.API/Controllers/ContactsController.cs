using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ECommerce.API.Repository;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly IContactRepository _contactRepository;
    private readonly IEmailRepository _emailRepository;
    private readonly ILogger<ContactsController> _logger;

    public ContactsController(IContactRepository contactRepository, ILogger<ContactsController> logger, IEmailRepository emailRepository)
    {
        _contactRepository = contactRepository;
        _logger = logger;
        _emailRepository = emailRepository;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> GetAllWithPagination([FromQuery] PaginationParameters paginationParameters,
          CancellationToken cancellationToken)
    {
        
            if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
            var entity = await _contactRepository.Search(paginationParameters, cancellationToken);
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
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<Contact>> GetById(int id, CancellationToken cancellationToken)
    {
        
            var result = await _contactRepository.GetByIdAsync(cancellationToken, id);
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
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<Contact>> GetByName(string name, CancellationToken cancellationToken)
    {
        
            var result = await _contactRepository.GetByName(name, cancellationToken);
            if (result == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.NotFound,
                    Messages = new List<string> { "پیامی برای این نام یافت نشد" }
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = result
            });
    }

    [HttpGet]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<Contact>> GetByEmail(string email, CancellationToken cancellationToken)
    {
        
            var result = await _contactRepository.GetByEmail(email, cancellationToken);
            if (result == null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.NotFound,
                    Messages = new List<string> { "پیامی برای این ایمیل یافت نشد" }
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = result
            });
    }

    [HttpPost]
    public async Task<IActionResult> Post(Contact contact, CancellationToken cancellationToken)
    {
        
            var result = await _contactRepository.GetRepetitive(contact, cancellationToken);
            if (result != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest,
                    Messages = new List<string> { "پیام تکراری از این فرستنده تکراری است" }
                });

            if (string.IsNullOrEmpty(contact.Email) || string.IsNullOrEmpty(contact.Message))
                return Ok(new ApiResult
                {
                    Code = ResultCode.BadRequest,
                    Messages = new List<string> { "ایمیل و متن باید وارد شود" }
                });

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = await _contactRepository.AddAsync(contact, cancellationToken)
            });
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(Contact contact, CancellationToken cancellationToken)
    {
        
            await _contactRepository.UpdateAsync(contact, cancellationToken);
            await _emailRepository.SendEmailAsync(contact.Email, "پاسخ به تماس با ما", contact.ReplayMessage, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        
            await _contactRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
    }
}