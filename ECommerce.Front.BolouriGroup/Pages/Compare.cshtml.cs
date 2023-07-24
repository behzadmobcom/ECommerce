using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages;

public class CompareModel : PageModel
{
    private readonly ICompareService _compareService;
    public List<int> CategoryId = new List<int>(); 
    public CompareModel(ICompareService compareService)
    {
        _compareService = compareService;
    }
    public List<ProductCompareViewModel>? ProductsList { get; set; }
    public ProductCompareViewModel? CompareProduct { get; set; }
    public string Message { get; set; }

    public async Task<IActionResult> OnGetAsync(List<int> productListId)
    {
        Message = "";
        var result = await _compareService.CompareList(productListId);
        if (result.Code > 0 || result.ReturnData.Count == 0)
        {
            Message = "ابتدا کالایی برای مقایسه انتخاب کنید";
            return RedirectToPage("/index", new { message = result.Message, code = result.Code.ToString() });
        }
        foreach (var compare in result.ReturnData)
        {
            foreach (int category in compare.ProductCategories)
            {
                CategoryId.Add(category);
            }
        }
        var CategoriesResult = await _compareService.GetProductsByCategories(CategoryId);
        ProductsList = CategoriesResult.ReturnData;
        CompareProduct = result.ReturnData.First();
        int Index = ProductsList.FindIndex(a => a.Id == productListId.First());
        ProductsList.RemoveAt(Index);
        return Page();
    }
}