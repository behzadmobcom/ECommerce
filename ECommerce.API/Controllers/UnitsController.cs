using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UnitsController : ControllerBase
{
    private readonly IHolooUnitRepository _holooUnitRepository;
    private readonly ILogger<UnitsController> _logger;
    private readonly IUnitRepository _unitRepository;

    public UnitsController(IUnitRepository unitRepository, IHolooUnitRepository holooUnitRepository,
        ILogger<UnitsController> logger)
    {
        _unitRepository = unitRepository;
        _holooUnitRepository = holooUnitRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {

        if (string.IsNullOrEmpty(paginationParameters.Search)) paginationParameters.Search = "";
        var entity = await _unitRepository.Search(paginationParameters, cancellationToken);
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
    public async Task<IActionResult> GetHolooUnits(CancellationToken cancellationToken)
    {

        return Ok(new ApiResult
        {
            Code = ResultCode.Success,
            ReturnData = await _holooUnitRepository.GetAll(cancellationToken)
        });
    }

    [HttpGet]
    public async Task<ActionResult<Unit>> GetById(int id, CancellationToken cancellationToken)
    {

        var result = await _unitRepository.GetByIdAsync(cancellationToken, id);
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
    public async Task<IActionResult> Post(Unit unit, CancellationToken cancellationToken)
    {

        if (unit == null)
            return Ok(new ApiResult
            {
                Code = ResultCode.BadRequest
            });
        unit.Name = unit.Name.Trim();

        var repetitiveUnit = await _unitRepository.GetByName(unit.Name, cancellationToken);
        if (repetitiveUnit != null)
            return Ok(new ApiResult
            {
                Code = ResultCode.Repetitive,
                Messages = new List<string> { "نام واحد تکراری است" }
            });

        return Ok(new ApiResult
        {
            Code = ResultCode.Success,
            ReturnData = await _unitRepository.AddAsync(unit, cancellationToken)
        });
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(Unit unit, CancellationToken cancellationToken)
    {

        if (unit.Id == 1)
            return Ok(new ApiResult
            {
                Code = ResultCode.BadRequest,
                Messages = new List<string> { "واحد پیشفرض قابل ویرایش نیست" }
            });
        var repetitive = await _unitRepository.GetByName(unit.Name, cancellationToken);
        if (repetitive != null && repetitive.Id != unit.Id)
            return Ok(new ApiResult
            {
                Code = ResultCode.Repetitive,
                Messages = new List<string> { "نام واحد تکراری است" }
            });
        if (repetitive != null) _unitRepository.Detach(repetitive);
        await _unitRepository.UpdateAsync(unit, cancellationToken);
        return Ok(new ApiResult
        {
            Code = ResultCode.Success
        });
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {

        if (id == 1)
            return Ok(new ApiResult
            {
                Code = ResultCode.BadRequest,
                Messages = new List<string> { "واحد پیشفرض قابل حذف نیست" }
            });
        await _unitRepository.DeleteAsync(id, cancellationToken);
        return Ok(new ApiResult
        {
            Code = ResultCode.Success
        });
    }

    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> ConvertHolooToSunflower(CancellationToken cancellationToken)
    {

        var units = (await _holooUnitRepository.GetAll(cancellationToken))!.Select(x => new Unit
        {
            Name = x.Unit_Name,
            Few = x.Unit_Few,
            UnitCode = x.Unit_Code,
            assay = x.Ayar,
            UnitWeight = x.Vahed_Vazn
        });


        await _unitRepository.AddRangeAsync(units, cancellationToken);

        return Ok(new ApiResult
        {
            Code = ResultCode.Success
        });
    }
}