using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

using Entities;
using Entities.Helper;
using Entities.HolooEntity;
using Entities.ViewModel;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.Products
{
    public class AttributeValueModel : PageModel
    {
        [TempData] public string Message { get; set; }
        [TempData] public string Code { get; set; }

        private readonly IProductAttributeGroupService _attributeGroupService;

        public List<ProductAttributeGroup> AttributeGroups { get; set; }
        [BindProperty] public int ProductId { get; set; }

        public AttributeValueModel(IProductAttributeGroupService attributeGroupService)
        {
            _attributeGroupService = attributeGroupService;
        }
        public async Task OnGet(int id, string search = "", int pageIndex = 1, int quantityPerPage = 10, string message = null, string code = null)
        {
            Message = message;
            Code = code;
            ProductId = id;
            var result = await _attributeGroupService.GetByProductId(id);
            if (result.Code == ServiceCode.Success)
            {
                AttributeGroups = result.ReturnData;

            }
        }

        public async Task<IActionResult> OnPost(List<ProductAttributeGroup> attributeGroups)
        {
            var result  =await _attributeGroupService.AddWithAttributeValue(attributeGroups, ProductId);
            return RedirectToAction("/Products/AttributeValue", new { id = ProductId });
        }

    }
}