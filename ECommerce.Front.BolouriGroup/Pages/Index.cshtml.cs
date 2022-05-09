using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Entities;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.IServices;

namespace Bolouri.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICartService _cartService;
        private readonly ICompareService _compareService;
        private readonly ICookieService _cookieService;
        private readonly IProductService _productService;
        private readonly ISlideShowService _slideShowService;
        private readonly IWishListService _wishListService;
        private readonly IBrandService _brandService;

        public IndexModel(ISlideShowService slideShowService, IProductService productService,
            IWishListService wishListService, ICartService cartService, ICompareService compareService, ICookieService cookieService, IBrandService brandService)
        {
            _slideShowService = slideShowService;
            _productService = productService;
            _wishListService = wishListService;
            _cartService = cartService;
            _compareService = compareService;
            _cookieService = cookieService;
            _brandService = brandService;
        }

        public List<SlideShowViewModel> SlideShowViewModels { get; set; }
        public List<ProductIndexPageViewModel> NewProducts { get; set; }
        public List<ProductIndexPageViewModel> ExpensiveProducts { get; set; }
        public List<ProductIndexPageViewModel> StarProducts { get; set; }
        public List<ProductIndexPageViewModel> NewTopProducts { get; set; }
        public List<ProductIndexPageViewModel> SellProducts { get; set; }
        public List<Brand> Brands { get; set; }

        public bool IsColleague { get; set; }

        public async Task OnGetAsync()
        {
            SlideShowViewModels = (await _slideShowService.TopSlideShow(5)).ReturnData;
            //NewProducts = (await _productService.TopProducts()).ReturnData;
            //ExpensiveProducts = (await _productService.TopProducts("", 0, 4, 4)).ReturnData;
            //StarProducts = (await _productService.TopProducts("", 0, 10, 2)).ReturnData;
            //SellProducts = (await _productService.TopProducts("", 0, 10, 5)).ReturnData;
            //NewTopProducts = (await _productService.TopProducts()).ReturnData;
            Brands = (await _brandService.Load()).ReturnData;

            var result = _cookieService.GetCurrentUser();
            if (result.Id > 0) IsColleague = result.IsColleague;
            IsColleague = false;

            NewProducts = (await _productService.TopProducts()).ReturnData;
            ExpensiveProducts = NewProducts;
            StarProducts = NewProducts;
            SellProducts = NewProducts;
            NewTopProducts = NewProducts;
        }

        //public async Task<IActionResult> OnGetAddWishList(int id)
        //{
        //    var result = await _wishListService.Add(id);
        //    return new JsonResult(result.ToString());
        //}

        //public IActionResult OnGetAddCompareList(int id)
        //{
        //    var result = _compareService.Add(HttpContext, id);
        //    return new JsonResult(result.Message);
        //}

        //public async Task<JsonResult> OnGetAddCart(int id)
        //{
        //    var result = await _cartService.Add(HttpContext, id);
        //    return new JsonResult(result);
        //}

        //public async Task<JsonResult> OnGetLoadCart(int id)
        //{
        //    var result = await _cartService.Load(HttpContext);
        //    return new JsonResult(result);
        //}

        public IActionResult OnPost()
        {
            var stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            var ret = "";
            using (var reader = new StreamReader(stream))
            {
                var requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                    ret = requestBody;
                // do something
            }

            return new JsonResult(ret);
        }

        //public async Task<JsonResult> OnGetDeleteCart(int id)
        //{
        //    var result = await _cartService.Delete(HttpContext, id);
        //    return new JsonResult(result);
        //}

        public async Task<IActionResult> OnGetLogout()
        {
            await _cookieService.LogOut();
            return RedirectToPage("/index");
        }
    }
}
