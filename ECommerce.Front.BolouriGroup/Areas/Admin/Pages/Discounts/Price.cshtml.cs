using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.HolooEntity;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.Discounts;

public class PriceModel : PageModel
{
    private readonly IPriceService _priceService;
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private readonly IDiscountService _discountService;

    public PriceModel(IPriceService priceService, ICategoryService categoryService, IProductService productService, IDiscountService discountService )
    {
        _priceService = priceService;
        _categoryService = categoryService;
        _productService = productService;
        _discountService = discountService;
    }
    [BindProperty] public DiscountViewModel Discount { get; set; }
    [BindProperty] public bool WithPrice { get; set; } 
    [BindProperty] public List<CategoryParentViewModel> CategoryParentViewModel { get; set; } = new();
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    private async Task Initial(string message = null, string code = null)
    {
        Message = message;
        Code = code;
        //CategoryParentViewModel.Add(new CategoryParentViewModel { Name = "انتخاب دسته اصلی" });
        CategoryParentViewModel.AddRange((await _categoryService.GetParents()).ReturnData);
    }

    public async Task OnGet(string message = null, string code = null)
    {       
        await Initial(message, code);
    }

    public async Task<IActionResult> OnPost()
    {
        if (WithPrice && !Discount.Amount.HasValue) ModelState.AddModelError(string.Empty, "مبلغ نباید خالی باشد.");
        if (!WithPrice && !Discount.Percent.HasValue) ModelState.AddModelError(string.Empty, "درصد نباید خالی باشد.");
        if (!Discount.StartDate.HasValue) ModelState.AddModelError(string.Empty, "تاریخ شروع نباید خالی باشد.");
        if (!Discount.EndDate.HasValue) ModelState.AddModelError(string.Empty, "تاریخ پایان نباید خالی باشد.");
        if (Discount.StartDate > Discount.EndDate) ModelState.AddModelError(string.Empty, "تاریخ پایان نباید قبل از تاریخ شروع باشد.");
        if (Discount.PricesId.Count == 0) ModelState.AddModelError(string.Empty, "حداقل یک کالا را انتخاب نمایید.");
        if (ModelState.IsValid)
        {
            if (WithPrice) Discount.Percent = null;
            else Discount.Amount = null;
            var result = await _discountService.Add(Discount);
            if (result.Code == 0)
                return RedirectToPage("/Discounts/Index",
                    new { area = "Admin", message = result.Message, code = result.Code.ToString() });
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }
        await Initial(Message, Code);
        return Page();
    }

    public async Task<JsonResult> OnGetReturnProductByCategoryId(int categoryId)
    {
        var result = await _productService.GetByCategoryId(categoryId);
        if (result.Code == ServiceCode.Success) return new JsonResult(result.ReturnData);
        return new JsonResult(new List<HolooSGroup>());
    }

    public async Task<JsonResult> OnGetReturnArticle(int code)
    {
        var result = await _priceService.PriceOfProduct(code);
        if (result.Code == ServiceCode.Success) return new JsonResult(result.ReturnData);
        return new JsonResult(new List<Product>());
    }   
}

