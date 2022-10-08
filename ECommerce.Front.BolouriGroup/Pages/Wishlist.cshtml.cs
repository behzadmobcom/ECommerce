using ECommerce.Services.IServices;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bolouri.Pages;

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