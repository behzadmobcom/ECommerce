using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.ProductAttributes
{
    public class DetailModel : PageModel
    {
        private readonly IProductAttributeService _productAttributeService;

        public DetailModel(IProductAttributeService productAttributeService)
        {
            _productAttributeService = productAttributeService;
        }

        public ProductAttribute ProductAttribute { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var result = await _productAttributeService.GetById(id);
            if (result.Code == 0)
            {
                ProductAttribute = result.ReturnData;
                return Page();
            }

            return RedirectToPage("/ProductAttributes/Index",
                new {area = "Admin", message = result.Message, code = result.Code.ToString()});
        }
    }
}