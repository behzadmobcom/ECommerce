using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace ArshaHamrah.Pages;

public class CartModel : PageModel
{
    private readonly ICartService _cartService;

    public CartModel(ICartService cartService, IWishListService wishListService, ICityService cityService,
        IStateService stateService)
    {
        _cartService = cartService;
    }

    public ServiceResult<List<PurchaseOrderViewModel>> CartList { get; set; }

    public async Task OnGetAsync()
    {
        CartList = await _cartService.Load(HttpContext);
    }

    public async Task<JsonResult> OnGetRemoveCart(int id)
    {
        var result = await _cartService.Delete(HttpContext, id);
        return new JsonResult(result);
    }
}