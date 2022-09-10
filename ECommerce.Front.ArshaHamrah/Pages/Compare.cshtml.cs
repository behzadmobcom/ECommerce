using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;

namespace ArshaHamrah.Pages;

public class CompareModel : PageModel
{
    private readonly ICompareService _compareService;

    public CompareModel(ICompareService compareService)
    {
        _compareService = compareService;
    }

    public List<ProductCompareViewModel> CompareProducts { get; set; }
    public string Message { get; set; }

    public async Task OnGetAsync()
    {
        Message = "";
        var result = await _compareService.CompareList(HttpContext);
        if (result.Code > 0 || result.ReturnData.Count == 0)
        {
            Message = "ابتدا کالایی برای مقایسه انتخاب کنید";
            return;
        }

        CompareProducts = result.ReturnData;
    }
}