using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages;

public class WishlistModel : PageModel
{
    private readonly IWishListService _wishListService;

    public WishlistModel(IWishListService wishListService)
    {
        _wishListService = wishListService;
    }

    public ServiceResult<List<WishListViewModel>> WishList { get; set; }

    public async Task OnGet()
    {
        WishList = await _wishListService.Load();
    }
}