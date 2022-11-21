using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.ArshaHamrah.Pages;

public class CartModel : PageModel
{
    private readonly ICartService _cartService;
    public ServiceResult<List<PurchaseOrderViewModel>> CartList { get; set; }

    public string Message { get; set; }
    public string Code { get; set; }

    public CartModel(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task OnGetAsync(string? message = null)
    {
        if (message != null)
        {
            Message = message;
            Code = "Error";
        }
        CartList = await _cartService.Load(HttpContext);
    }

    public async Task<ActionResult> OnPost()
    {
        var cartList = await _cartService.Load(HttpContext);
        if (cartList.ReturnData.Count > 0)
        {
            return RedirectToPage("Checkout");
        }

        CartList = cartList;
        Message = "هیچ کالایی انتخاب نشده است";
        Code = "Error";
        return Page();
    }
}