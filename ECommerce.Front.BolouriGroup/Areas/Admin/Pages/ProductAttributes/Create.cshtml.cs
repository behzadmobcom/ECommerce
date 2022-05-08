using System.Threading.Tasks;
using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.ProductAttributes
{
    public class CreateModel : PageModel
    {
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductAttributeGroupService _productAttributeGroupService;

        public CreateModel(IProductAttributeService productAttributeService, IProductAttributeGroupService productAttributeGroupService)
        {
            _productAttributeService = productAttributeService;
            _productAttributeGroupService = productAttributeGroupService;
        }

        [BindProperty] public ProductAttribute ProductAttribute { get; set; }
        public SelectList AttributeGroup { get; set; }

        [TempData] public string Message { get; set; }

        [TempData] public string Code { get; set; }

        public async Task OnGet()
        {
            var attributeGroups =(await _productAttributeGroupService.GetAll()).ReturnData;
            AttributeGroup = new SelectList(attributeGroups, nameof(ProductAttributeGroup.Id), nameof(ProductAttributeGroup.Name));
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await _productAttributeService.Add(ProductAttribute);
                if (result.Code == 0)
                    return RedirectToPage("/ProductAttributes/Index",
                        new {area = "Admin", message = result.Message, code = result.Code.ToString()});
                Message = result.Message;
                Code = result.Code.ToString();
                ModelState.AddModelError("", result.Message);
            }

            return Page();
        }
    }
}