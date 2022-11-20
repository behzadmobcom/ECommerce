using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Front.BolouriGroup.Areas.Admin.Pages.Blogs;

public class CreateModel : PageModel
{
    private readonly IBlogService _blogService;
    private readonly IHostEnvironment _environment;
    private readonly IImageService _imageService;
    private readonly ITagService _tagService;
    private readonly IKeywordService _keywordService;
    private readonly IBlogAuthorService _blogAuthorService;
    private readonly IBlogCategoryService _blogCategoryService;

    public CreateModel(IBlogService blogService, IImageService imageService, ITagService tagService,
        IKeywordService keywordService, IHostEnvironment environment, IBlogAuthorService blogAuthorService, IBlogCategoryService blogCategoryService)
    {
        _blogService = blogService;
        _imageService = imageService;
        _environment = environment;
        _keywordService = keywordService;
        _tagService = tagService;
        _blogAuthorService = blogAuthorService;
        _blogCategoryService = blogCategoryService;
    }

    [BindProperty] public BlogViewModel Blog { get; set; }

    [BindProperty] public IFormFile? Upload { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }
    public SelectList Tags { get; set; }
    public SelectList Keywords { get; set; }
    public SelectList BlogAuthors { get; set; }
    public List<CategoryParentViewModel> Categories { get; set; }

    private async Task Initial()
    {
        var tags = (await _tagService.GetAll()).ReturnData;
        Tags = new SelectList(tags, nameof(Tag.Id), nameof(Tag.TagText));

        var keywords = (await _keywordService.GetAll()).ReturnData;
        Keywords = new SelectList(keywords, nameof(Keyword.Id), nameof(Keyword.KeywordText));

        var blogAuthors = (await _blogAuthorService.GetAll()).ReturnData;
        BlogAuthors = new SelectList(blogAuthors, nameof(BlogAuthor.Id), nameof(BlogAuthor.Name));

        var resultParent = await _blogCategoryService.GetParents();
        Categories = resultParent.ReturnData;
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
        
        if (ModelState.IsValid)
        {
            var result = await _blogService.Add(Blog);
            if (result.Code == 0)
            {
                var resultImage = await _imageService.Add(Upload, result.ReturnData.Id, "Images/Blogs",
                    _environment.ContentRootPath);
                if (resultImage.Code == 0)
                {
                    return RedirectToPage("/Blogs/Index",
                        new { area = "Admin", message = result.Message, code = result.Code.ToString() });
                }
                Message = result.Message;
                Code = result.Code.ToString();
                ModelState.AddModelError("", result.Message);
            }
        }
        await Initial();
        return Page();
    }
}