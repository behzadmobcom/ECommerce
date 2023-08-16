using Ecommerce.Entities;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.Discounts;

public class CreateModel : PageModel
{
    private readonly IDiscountService _discountService;
    private readonly IProductService _productService;
    private readonly IPriceService _priceService;
    private readonly ICategoryService _categoryService;    

    public CreateModel(IDiscountService discountService, IProductService productService, IPriceService priceService
        ,ICategoryService categoryService)
    {
        _discountService = discountService;
        _productService = productService;
        _priceService = priceService;
        _categoryService = categoryService;
    }

    [BindProperty] public Discount Discount { get; set; }
    [BindProperty] public bool WithPrice { get; set; }
    [BindProperty] public DiscountViewModel Discount { get; set; }
    public SelectList Products { get; set; }
    public SelectList Prices { get; set; }
    public SelectList Categories { get; set; }


    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public async Task OnGet()
    {
        var products = (await _productService.GetAll()).ReturnData;
        Products = new SelectList(products, nameof(ProductViewModel.Id), nameof(ProductViewModel.Name));

        var categories = (await _categoryService.GetAll()).ReturnData;
        Categories = new SelectList(categories, nameof(Category.Id), nameof(Category.Name));

        var prices = (await _priceService.GetAllByViewModel()).ReturnData;
        Prices = new SelectList(prices.Select(x => new { x.Id, 
                                             Description = x.ProductName + " " + x.ColorName + " " + x.Amount.ToString("N0") }),
                                             "Id", "Description");
    }

    public async Task<IActionResult> OnPost()
    {
        if (WithPrice && !Discount.Amount.HasValue) ModelState.AddModelError(string.Empty, "مبلغ نباید خالی باشد.");
        if (!WithPrice && !Discount.Percent.HasValue) ModelState.AddModelError(string.Empty, "درصد نباید خالی باشد.");
        if (!Discount.StartDate.HasValue) ModelState.AddModelError(string.Empty, "تاریخ شروع نباید خالی باشد.");
        if (!Discount.EndDate.HasValue) ModelState.AddModelError(string.Empty, "تاریخ پایان نباید خالی باشد.");
        if (Discount.StartDate > Discount.EndDate) ModelState.AddModelError(string.Empty, "تاریخ پایان نباید قبل از تاریخ شروع باشد.");
        if (Discount.MinOrder > Discount.MaxOrder) ModelState.AddModelError(string.Empty, "حداقل تعداد سفارش باید کم تر از حداکثر آن باشد.");

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

        return Page();
    }
}