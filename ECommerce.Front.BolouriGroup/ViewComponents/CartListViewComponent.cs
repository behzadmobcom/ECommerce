using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Front.BolouriGroup.ViewComponents;

public class CartListViewComponent : ViewComponent
{
    private readonly ICartService _cartService;

    public CartListViewComponent(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var cart = (await _cartService.Load(HttpContext)).ReturnData;
        TempData["cartLength"] = cart.Count;
        return View(cart);
    }
}