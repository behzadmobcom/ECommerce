﻿using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bolouri.Areas.Admin.Pages.Blogs;

public class CreateModel : PageModel
{
    private readonly IBlogService _blogService;
    private readonly IHostEnvironment _environment;
    private readonly IImageService _imageService;
    private readonly ITagService _tagService;
    private readonly IKeywordService _keywordService;
    private readonly IBlogAuthorService _blogAuthorService;

    public CreateModel(IBlogService brandService, IImageService imageService, ITagService tagService,
        IKeywordService keywordService, IHostEnvironment environment, IBlogAuthorService blogAuthorService)
    {
        _blogService = brandService;
        _imageService = imageService;
        _environment = environment;
        _keywordService = keywordService;
        _tagService = tagService;
        _blogAuthorService = blogAuthorService;
       
    }

    [BindProperty] public Blog Blog { get; set; }

    [BindProperty] public IFormFile Upload { get; set; }

    [TempData] public string Message { get; set; }

    [TempData] public string Code { get; set; }
    public SelectList Tags { get; set; }
    public SelectList Keywords { get; set; }
    public SelectList BlogAuthors { get; set; }

    private async Task Initial()
    {
        var tags = (await _tagService.GetAll()).ReturnData;
        Tags = new SelectList(tags, nameof(Tag.Id), nameof(Tag.TagText));

        var keywords = (await _keywordService.GetAll()).ReturnData;
        Keywords = new SelectList(keywords, nameof(Keyword.Id), nameof(Keyword.KeywordText));

        var blogAuthors = (await _blogAuthorService.GetAll()).ReturnData;
        BlogAuthors = new SelectList(blogAuthors, nameof(BlogAuthor.Id), nameof(BlogAuthor.Name));
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
                return RedirectToPage("/Blogs/Index",
                    new {area = "Admin", message = result.Message, code = result.Code.ToString()});
            Message = result.Message;
            Code = result.Code.ToString();
            ModelState.AddModelError("", result.Message);
        }

        return Page();
    }
}