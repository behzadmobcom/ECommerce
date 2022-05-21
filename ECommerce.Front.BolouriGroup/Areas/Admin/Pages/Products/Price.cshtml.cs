using Entities;
using Entities.Helper;
using Entities.HolooEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ECommerce.Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.Products;

public class PriceModel : PageModel
{
    private readonly IColorService _colorService;
    private readonly ICurrencyService _currencyService;
    private readonly IHolooArticleService _holooArticleService;
    private readonly IHolooMGroupService _holooMGroupService;
    private readonly IHolooSGroupService _holooSGroupService;
    private readonly IPriceService _priceService;
    private readonly ISizeService _sizeService;
    private readonly IUnitService _unitService;

    public PriceModel(IPriceService priceService, IUnitService unitService, ISizeService sizeService,
        IColorService colorService, ICurrencyService currencyService, IHolooMGroupService holooMGroupService,
        IHolooSGroupService holooSGroupService, IHolooArticleService holooArticleService)
    {
        _priceService = priceService;
        _unitService = unitService;
        _sizeService = sizeService;
        _colorService = colorService;
        _currencyService = currencyService;
        _holooMGroupService = holooMGroupService;
        _holooSGroupService = holooSGroupService;
        _holooArticleService = holooArticleService;
    }

    public SelectList Units { get; set; }
    public SelectList Sizes { get; set; }
    public SelectList Colors { get; set; }
    public SelectList Currencies { get; set; }
    [BindProperty] public Price Price { get; set; } = new();
    public ServiceResult<List<Price>> Prices { get; set; }
    public List<HolooMGroup> HolooMGroups { get; set; } = new();
    public List<HolooSGroup> HolooSGroups { get; set; } = new();
    public List<Product> HolooArticle { get; set; } = new();

    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task OnGet(int id, string search = "", int pageIndex = 1, int quantityPerPage = 10,
        string message = null, string code = null)
    {
        Message = message;
        Code = code;

        await Initial(id, search, pageIndex, quantityPerPage, message, code);
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            ServiceResult result;
            if (Price.Id > 0)
                result = await _priceService.Edit(Price);
            else
                result = await _priceService.Add(Price);
            if (result.Code == 0)
                return RedirectToPage("/Products/Price",
                    new
                    {
                        area = "Admin", id = Price.ProductId, message = result.Message, code = result.Code.ToString()
                    });
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        await Initial(Price.ProductId);
        return Page();
    }

    public async Task<IActionResult> OnPostDelete(int id, int productId)
    {
        var result = await _priceService.Delete(id);
        if (result.Code == 0)
            return RedirectToPage("/Products/Price",
                new {area = "Admin", id = productId, message = result.Message, code = result.Code.ToString()});
        Message = result.Message;
        Code = result.Code.ToString();
        await Initial(productId);
        return Page();
    }

    public async Task<IActionResult> OnPostEdit(int id, int productId)
    {
        await Initial(productId);
        Price = Prices.ReturnData.First(x => x.Id == id);
        return Page();
    }

    public async Task<JsonResult> OnGetReturnSGroup(string mCode)
    {
        var result = await _holooSGroupService.Load(mCode);
        if (result.Code == ServiceCode.Success) return new JsonResult(result.ReturnData);

        return new JsonResult(new List<HolooSGroup>());
    }

    public async Task<JsonResult> OnGetReturnArticle(string code)
    {
        var result = await _holooArticleService.Load(code);
        if (result.Code == ServiceCode.Success) return new JsonResult(result.ReturnData);

        return new JsonResult(new List<Product>());
    }

    private async Task Initial(int productId, string search = "", int pageNumber = 1, int pageSize = 10,
        string message = null, string code = null)
    {
        Price.ProductId = productId;
        var units = (await _unitService.Load()).ReturnData;
        Units = new SelectList(units, nameof(Unit.Id), nameof(Unit.Name));

        var sizes = (await _sizeService.Load()).ReturnData;
        Sizes = new SelectList(sizes, nameof(Size.Id), nameof(Size.Name));

        var colors = (await _colorService.Load()).ReturnData;
        Colors = new SelectList(colors, nameof(Color.Id), nameof(Color.Name));

        var currencies = (await _currencyService.Load()).ReturnData;
        Currencies = new SelectList(currencies, nameof(Currency.Id), nameof(Currency.Name));

        var result = await _priceService.Load(productId.ToString(), pageNumber, pageSize);
        if (result.Code == ServiceCode.Success) Prices = result;

        HolooMGroups.Add(new HolooMGroup {M_groupname = "انتخاب گروه اصلی"});
        HolooMGroups.AddRange((await _holooMGroupService.Load()).ReturnData);
        HolooSGroups.Add(new HolooSGroup {S_groupname = "ابتدا گروه اصلی را انتخاب کنید"});
        HolooArticle.Add(new Product {Name = "ابتدا گروه فرعی را انتخاب کنید"});
    }
}