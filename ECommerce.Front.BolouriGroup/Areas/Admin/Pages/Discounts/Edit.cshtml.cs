using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.Discounts;

public class EditModel : PageModel
{
    private readonly IDiscountService _discountService;

    public EditModel(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    [BindProperty] public Discount Discount { get; set; }
    [BindProperty] public bool WithPrice { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }

    public async Task OnGet(int id)
    {
        var result = await _discountService.GetById(id);
        Discount = result.ReturnData;
        if (!Discount.Amount.HasValue) WithPrice = false;
        else if (!Discount.Percent.HasValue) WithPrice = true;
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
            var result = await _discountService.Edit(Discount);
            Message = result.Message;
            Code = result.Code.ToString();
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