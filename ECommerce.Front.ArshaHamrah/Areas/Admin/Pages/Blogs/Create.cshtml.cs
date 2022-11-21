using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Front.ArshaHamrah.Areas.Admin.Pages.Blogs;

[Authorize(Roles = "Admin,SuperAdmin")]
public class CreateModel : PageModel
{
    private readonly IBlogService _blogService;
    private readonly IHostEnvironment _environment;
    private readonly IImageService _imageService;
    private readonly IBlogCategoryService _blogCategoryService;
    private readonly IBlogAuthorService _blogAuthorService;
    private readonly ITagService _tagService;
    private readonly IKeywordService _keywordService;

    public CreateModel(IBlogService brandService, IImageService imageService, 
                        IHostEnvironment environment , IBlogCategoryService blogCategoryService,
                        IBlogAuthorService blogAuthorService, ITagService tagService, IKeywordService keywordService)
    {
        _blogService = brandService;
        _imageService = imageService;
        _environment = environment;
        _blogCategoryService = blogCategoryService;
        _blogAuthorService = blogAuthorService;
        _tagService = tagService;
        _keywordService = keywordService;
    }

    [BindProperty] public BlogViewModel Blog { get; set; }

    [BindProperty] public IFormFile Upload { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }

    public SelectList BlogCategories { get; set; }
    public SelectList BlogAuthors { get; set; }
    public SelectList Tags { get; set; }
    public SelectList Keywords { get; set; }

    private async Task Initial()
    {       
        var blogcategories = (await _blogCategoryService.GetAll()).ReturnData;
        BlogCategories = new SelectList(blogcategories, nameof(Store.Id), nameof(Store.Name));

        var blogauthors = (await _blogAuthorService.Load()).ReturnData;
        BlogAuthors = new SelectList(blogauthors, nameof(Store.Id), nameof(Store.Name));

        var tags = (await _tagService.GetAll()).ReturnData;
        Tags = new SelectList(tags, nameof(Tag.Id), nameof(Tag.TagText));

        var keywords = (await _keywordService.GetAll()).ReturnData;
        Keywords = new SelectList(keywords, nameof(Keyword.Id), nameof(Keyword.KeywordText));
    }

    public async Task OnGet()
    {
        await Initial();
    }

    public async Task<IActionResult> OnPost()
    {
        if (Upload == null)
        {
            Message = "لطفا عکس را انتخاب کنید";
            Code = ServiceCode.Error.ToString();
            return Page();
        }

        var fileName = (await _imageService.Upload(Upload, "Images/Blogs", _environment.ContentRootPath))
            .ReturnData;
        //Blog.ImagePath = $"/{fileName[0]}/{fileName[1]}/{fileName[2]}";

        if (ModelState.IsValid)
        {
            var result = await _blogService.Add(Blog);
            if (result.Code == 0)
            {
              

                return RedirectToPage("/Blogs/Index",
                    new { area = "Admin", message = result.Message, code = result.Code.ToString() });
            }

            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        await Initial();
        return Page();
        //var result = await _blogService.Add(Blog);       

        //    var it = result.Code;
        //    if (result.Code == 0)
        //        return RedirectToPage("/Blogs/Index",
        //            new {area = "Admin", message = result.Message, code = result.Code.ToString()});
        //    Message = result.Message;
        //    Code = result.Code.ToString();
        //    ModelState.AddModelError("", result.Message);
        //}
        //Message=ModelState.ToString();
        //return Page();
    }
}