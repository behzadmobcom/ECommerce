using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages;

public class AboutUsModel : PageModel
{
    private readonly IBrandService _brandService;

    public AboutUsModel(IBrandService brandService)
    {
        _brandService = brandService;
    }
    public List<Brand> Brands { get; set; }

    public async Task OnGetAsync()
    {
        Brands = (await _brandService.Load()).ReturnData;
        Brands.RemoveAt(0);
    }

}