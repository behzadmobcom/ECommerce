using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace Bolouri.Pages
{
    public class ShopModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly ICartService _cartService;

        public ServiceResult<List<ProductIndexPageViewModel>> Products { get; set; }
        public Dictionary<int, string> Brands { get; set; }

        public ShopModel(ICategoryService categoryService, IProductService productService, IBrandService brandService, ICartService cartService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _brandService = brandService;
            _cartService = cartService;
        }

        public async Task OnGet(string path, int pageNumber = 1, int pageSize = 12, int productSort = 1, string message = null, string code = null)
        {
            string? categoryId = null;
            if (!string.IsNullOrEmpty(path))
            {
                var resultCategory = await _categoryService.GetByUrl(path);
                if (resultCategory.Code == ServiceCode.Success)
                {
                    categoryId =$"CategoryId={resultCategory.ReturnData.Id}";
                }
            }
            Products = await _productService.TopProducts(categoryId, pageNumber, pageSize, productSort);
            var brandResult = await _brandService.LoadDictionary();
            if (brandResult.Code == ServiceCode.Success)
            {
                Brands = brandResult.ReturnData;
            }
        }


        public async Task<JsonResult> OnGetAddCart(int id)
        {
            var result = await _cartService.Add(HttpContext, id);
            return new JsonResult(result);
        }

        public async Task<JsonResult> OnGetLoadCart(int id)
        {
            var result = await _cartService.Load(HttpContext);
            return new JsonResult(result);
        }

        public async Task<JsonResult> OnGetDeleteCart(int id)
        {
            var result = await _cartService.Delete(HttpContext, id);
            return new JsonResult(result);
        }
    }
}
