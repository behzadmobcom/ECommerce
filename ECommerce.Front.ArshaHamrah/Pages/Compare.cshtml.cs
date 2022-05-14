using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace ArshaHamrah.Pages;

public class CompareModel : PageModel
{
    private readonly ICompareService _compareService;

    public CompareModel(ICompareService compareService)
    {
        _compareService = compareService;
    }

    public List<ProductCompareViewModel> CompareProducts { get; set; }
    public int ProductCount { get; set; }
    public int GroupCount { get; set; }
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
        ProductCount = CompareProducts.Count;
        GroupCount = CompareProducts[0].AttributeGroupProducts.Count;
    }
}