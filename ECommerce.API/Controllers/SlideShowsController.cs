using API.Interface;
using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SlideShowsController : ControllerBase
{
    private readonly ILogger<SlideShowsController> _logger;
    private readonly IPriceRepository _priceRepository;
    private readonly ISettingRepository _settingRepository;
    private readonly ISlideShowRepository _slideShowRepository;

    public SlideShowsController(ISlideShowRepository slideShowRepository, IPriceRepository priceRepository,
        ISettingRepository settingRepository, ILogger<SlideShowsController> logger)
    {
        _slideShowRepository = slideShowRepository;
        _priceRepository = priceRepository;
        _settingRepository = settingRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        try
        {
            var slideShowsList = await _slideShowRepository.GetAllWithInclude(cancellationToken);
            var returnSlideShow = new List<SlideShowViewModel>();
            var slideShowViewModelList = slideShowsList.Select(s => new SlideShowViewModel
            {
                Id = s.Id,
                ProductId = s.ProductId,
                Title = s.Title,
                Description = s.Description,
                ImagePath = s.ImagePath,
                Name = s.Product.Name,
                Url = s.Product.Url
            });
            var currency = _settingRepository.IsDollar();
            foreach (var slideShow in slideShowViewModelList)
            {
                var temp = await _priceRepository.PriceOfProduct(slideShow.ProductId, cancellationToken);
                if (temp.Count() == 0) continue;
                slideShow.Price =
                    temp.FirstOrDefault(x => x.Currency.Name == currency && !x.IsColleague && x.MinQuantity == 1)
                        ?.Amount;
                returnSlideShow.Add(slideShow);
            }

            return Ok(new ApiResult
            {
                Code = ResultCode.Success,
                ReturnData = returnSlideShow
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
                {Code = ResultCode.DatabaseError, Messages = new List<string> {"اشکال در سمت سرور"}});
        }
    }

    [HttpGet]
    public async Task<ActionResult<SlideShow>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _slideShowRepository.GetByIdAsync(cancellationToken, id);
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
            return Ok(new ApiResult
                {Code = ResultCode.DatabaseError, Messages = new List<string> {"اشکال در سمت سرور"}});
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Post(SlideShow slideShow, CancellationToken cancellationToken)
    {
        try
        {
            if (slideShow == null) return BadRequest();
            slideShow.Title = slideShow.Title.Trim();

            var repetitiveTitle = await _slideShowRepository.GetByTitle(slideShow.Title, cancellationToken);
            if (repetitiveTitle != null)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"عنوان اسلاید شو تکراری است"}
                });

            var repetitiveProduct = _slideShowRepository.IsRepetitiveProduct(0,slideShow.ProductId, cancellationToken);
            if (repetitiveProduct)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"این کالا قبلا برای یک اسلاید شو دیگر انتخاب شده"}
                });
            var result = await _slideShowRepository.AddAsync(slideShow, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
                {Code = ResultCode.DatabaseError, Messages = new List<string> {"اشکال در سمت سرور"}});
        }
    }

    [HttpPut]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<bool>> Put(SlideShow slideShow, CancellationToken cancellationToken)
    {
        try
        {
            var repetitiveTitle = await _slideShowRepository.GetByTitle(slideShow.Title, cancellationToken);
            if (repetitiveTitle != null && repetitiveTitle.Id != slideShow.Id)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"عنوان اسلاید شو تکراری است"}
                });
            if (repetitiveTitle != null) _slideShowRepository.Detach(repetitiveTitle);

            var repetitiveProduct = _slideShowRepository.IsRepetitiveProduct(slideShow.Id, slideShow.ProductId, cancellationToken);
            if (repetitiveProduct)
                return Ok(new ApiResult
                {
                    Code = ResultCode.Repetitive,
                    Messages = new List<string> {"این کالا قبلا برای یک اسلاید شو دیگر انتخاب شده"}
                });
            await _slideShowRepository.UpdateAsync(slideShow, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
                {Code = ResultCode.DatabaseError, Messages = new List<string> {"اشکال در سمت سرور"}});
        }
    }

    [HttpDelete]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _slideShowRepository.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResult
            {
                Code = ResultCode.Success
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, e.Message);
            return Ok(new ApiResult
                {Code = ResultCode.DatabaseError, Messages = new List<string> {"اشکال در سمت سرور"}});
        }
    }
}