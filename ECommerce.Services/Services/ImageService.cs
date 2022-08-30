using Entities;
using Entities.Helper;
using Microsoft.AspNetCore.Http;
using ECommerce.Services.IServices;

namespace Services.Services;

public class ImageService : EntityService<Image>, IImageService
{
    private const string Url = "api/Images";
    private readonly HttpClient _http;

    public ImageService(IHttpService http, HttpClient http1) : base(http)
    {
        _http = http1;
    }

    public async Task<ServiceResult<List<string>>> Upload(IFormFile file, string path, string contentRootPath)
    {
        if (file == null || file.Length == 0)
            return new ServiceResult<List<string>>
            {
                Code = ServiceCode.Warning,
                Message = "لطفا یک فایل را انتخاب کنید"
            };

        var newPath = path.Split("/").ToList();

        var extension = Path.GetExtension(file.FileName).ToLower();

        string[] allowExtensions = { ".gif", ".jpg", ".jpeg", ".png" };

        if (!allowExtensions.Contains(extension.ToLower()))
            return new ServiceResult<List<string>>
            {
                Code = ServiceCode.Error,
                Message = "نوع فایل مورد قبول نمی باشد"
            };

        var newFileName = $"{Guid.NewGuid()}{extension}";
        //var newFileName = fileName[2];
        var filePath = Path.Combine(contentRootPath, "wwwroot", newPath[0], newPath[1], newFileName);

        await using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            await file.CopyToAsync(fileStream);
        }

        newPath.Add(newFileName);
        return new ServiceResult<List<string>>
        {
            Code = ServiceCode.Success,
            ReturnData = newPath
        };
    }

    public async Task<ServiceResult> Add(IFormFile file, int entityId, string path, string contentRootPath)
    {
        var uploadResult = await Upload(file, path, contentRootPath);
        if (uploadResult.Code > 0)
            return new ServiceResult
            {
                Code = uploadResult.Code,
                Message = uploadResult.Message
            };
        var fileName = uploadResult.ReturnData;
        var image = new Image();
        image.Name = fileName[2];
        image.Alt = file.FileName;
        image.Path = $"{fileName[0]}/{fileName[1]}";
        //var id = Convert.ToInt32(entityId);
        switch (fileName[1])
        {
            case "Products":
                image.ProductId = entityId;
                break;
            case "Blogs":
                image.BlogId = entityId;
                break;
        }

        var result = await Create(Url, image);
        return Return(result);
    }

    public async Task<ServiceResult> Edit(IFormFile file, Image image, string path, string contentRootPath)
    {
        var fileName = (await Upload(file, path, contentRootPath)).ReturnData;

        image.Name = fileName[2];
        image.Alt = fileName[2].Split(".")[0];
        image.Path = $"{fileName[0]}/{fileName[1]}";
        var id = Convert.ToInt32(fileName[3]);
        switch (fileName[1])
        {
            case "Products":
                image.ProductId = id;
                break;
            case "Blogs":
                image.BlogId = id;
                break;
        }

        var result = await Update(Url, image);
        return Return(result);
    }

    public async Task<ServiceResult> Delete(string imageName, int id, string contentRootPath)
    {
        var fileName = imageName.Split("/");
        var filePath = Path.Combine(contentRootPath, "wwwroot", fileName[0], fileName[1], fileName[2]);
        //if (!File.Exists(filePath)) return new ServiceResult {Code = ServiceCode.Error, Message = "عکس پیدا نشد"};
        if (File.Exists(filePath)) File.Delete(filePath);
        var result = await Delete(Url, id);
        return Return(result);
    }

    public async Task<ServiceResult<List<Image>>> GetImagesByProductId(int productId)
    {
        var result = await ReadList(Url, $"GetKeywordsByProductId?id={productId}");
        return Return(result);
    }
}