using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;
using Entities.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bolouri.Areas.Admin.Pages.Blogs;

public class EditModel : PageModel
{
    private readonly IBlogService _blogService;
    private readonly IHostEnvironment _environment;
    private readonly IImageService _imageService;
    private readonly ITagService _tagService;
    private readonly IKeywordService _keywordService;
    private readonly IBlogAuthorService _blogAuthorService;
    private readonly IBlogCategoryService _blogCategoryService;

    public EditModel(IBlogService blogService, IImageService imageService, ITagService tagService,
        IKeywordService keywordService, IHostEnvironment environment, IBlogAuthorService blogAuthorService, IBlogCategoryService blogCategoryService)
    {
        _blogService = blogService;
        _environment = environment;
        _imageService = imageService;
        _keywordService = keywordService;
        _tagService = tagService;
        _blogAuthorService = blogAuthorService;
        _blogCategoryService = blogCategoryService;
    }

    [BindProperty] public BlogViewModel Blog { get; set; }
    [BindProperty] public IFormFile? Upload { get; set; }
    [TempData] public string Message { get; set; }
    [TempData] public string Code { get; set; }
    public List<Tag> Tags { get; set; }
    public List<Keyword> Keywords { get; set; }
    public List<BlogAuthor> BlogAuthors { get; set; }
    public List<BlogCategory> Categories { get; set; }


    public async Task<IActionResult> OnGet(int id)
    {
        //var result = await _blogService.GetById(id);
        //Blog = result.ReturnData;        
        
        var result = await Initial(id);
        if (result.Code == 0) return Page();
        return RedirectToPage("/Blogs/Index",
            new { area = "Admin", message = result.Message, code = result.Code.ToString() });
    }

    public async Task<IActionResult> OnPost()
    {
        var _image = await _imageService.GetImagesByBlogId(Blog.Id);
        Blog.Image = _image.ReturnData;

        if (Upload != null && Blog.Image.Id!=0)
        {
            await _imageService.Delete($"Images/Blogs/{Blog.Image?.Name}", Blog.Image.Id, _environment.ContentRootPath);
            _image = await _imageService.GetImagesByBlogId(Blog.Id);
            Blog.Image = _image.ReturnData;
        }
        if (Upload != null && Blog.Image.Id == 0)
        {
            var resultImage = await _imageService.Add(Upload, Blog.Id, "Images/Blogs",
                       _environment.ContentRootPath);
            if (resultImage.Code > 0)
            {
                Message = resultImage.Message;
                Code = resultImage.Code.ToString();
                ModelState.AddModelError("", resultImage.Message);               
            }

            _image = await _imageService.GetImagesByBlogId(Blog.Id);
            Blog.Image = _image.ReturnData;
        }
        if (Upload == null && Blog.Image.Id == 0)
        {
            Message = "لطفا عکس را انتخاب کنید";
            Code = ServiceCode.Error.ToString();
            await Initial(Blog.Id);
            return Page();
        }

        if (ModelState.IsValid)
        {
            var result = await _blogService.Edit(Blog);
            if (result.Code == 0)
            {                  
                 
                return RedirectToPage("/Blogs/Index",
                    new { area = "Admin", message = result.Message, code = result.Code.ToString() });
            }

            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        await Initial(Blog.Id);
        return Page();
    }

    private async Task<ServiceResult<BlogViewModel>> Initial(int id)
    {
        var result = await _blogService.GetById(id);
        if (result.Code > 0)
            return new ServiceResult<BlogViewModel>
            {
                Code = result.Code,
                Message = result.Message
            };
        Blog = result.ReturnData;

        Tags = (await _tagService.GetAll()).ReturnData;
        Keywords = (await _keywordService.GetAll()).ReturnData;
        Categories = (await _blogCategoryService.GetAll()).ReturnData;
        BlogAuthors = (await _blogAuthorService.GetAll()).ReturnData;
        //BlogAuthors = new SelectList(blogAuthors, nameof(BlogAuthor.Id), nameof(BlogAuthor.Name));


        return result;
    }

    public async Task<IActionResult> OnPostDeleteImage(string imageName, int id, int blogId)
    {        
            var result = await _imageService.Delete($"Images/Blogs/{imageName}", id, _environment.ContentRootPath);

            if (result.Code == 0)
                //return RedirectToPage("/Blogs/Edit",
                //    new { area = "Admin", message = result.Message, code = result.Code.ToString() });
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        
        await Initial(blogId);
        return Page();
    }
}