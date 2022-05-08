using System.Threading.Tasks;
using Entities;
using Entities.Helper;
using Entities.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Services.IServices;

namespace ArshaHamrah.Areas.Admin.Pages.SlideShows
{
    public class CreateModel : PageModel
    {
        private readonly ISlideShowService _slideShowService;
        private readonly IHostEnvironment _environment;
        private readonly IImageService _imageService;
        private readonly IProductService _productService;

        public CreateModel(ISlideShowService slideShowService, IImageService imageService, IHostEnvironment environment,IProductService productService)
        {
            _slideShowService = slideShowService;
            _imageService = imageService;
            _environment = environment;
            _productService = productService;
        }

        [BindProperty] public SlideShow SlideShow { get; set; }

        [BindProperty] public IFormFile Upload { get; set; }

        public PaginationViewModel PaginationViewModel { get; set; }

        [TempData] public string Message { get; set; }

        [TempData] public string Code { get; set; }

        public async Task OnGet()
        {
            var result = await _productService.Search("", 1, 30);
            PaginationViewModel = result.ReturnData;
        }

        public async Task<IActionResult> OnPost()
        {
            var resultProduct = await _productService.Search("", 1, 30);
            PaginationViewModel = resultProduct.ReturnData;

            if (Upload == null)
            {
                Message = "لطفا عکس را انتخاب کنید";
                Code = ServiceCode.Error.ToString();
                return Page();
            }
            var fileName = (await _imageService.Upload(Upload, "Images/SlideShows", _environment.ContentRootPath))
                .ReturnData;
            SlideShow.ImagePath = $"/{fileName[0]}/{fileName[1]}/{fileName[2]}";
            ModelState.Remove("SlideShow.ImagePath");

            if (ModelState.IsValid)
            {
                var result = await _slideShowService.Add(SlideShow);
                if (result.Code == 0)
                    return RedirectToPage("/SlideShows/Index",
                        new {area = "Admin", message = result.Message, code = result.Code.ToString()});
                Message = result.Message;
                Code = result.Code.ToString();
                ModelState.AddModelError("", result.Message);
            }

            return Page();
        }

        public async Task<JsonResult> OnGetReturnProducts(string search = "")
        {
            var result = await _productService.Search(search, 1, 30);
            PaginationViewModel = result.ReturnData;
            return new JsonResult(PaginationViewModel.Products);
        }
    }
}