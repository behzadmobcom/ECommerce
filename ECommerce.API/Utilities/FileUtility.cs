namespace ECommerce.API.Utilities;

public static class FileUtility
{
    private const string BaseUploadPath = "upload";
    private const string BaseFullUploadPath = "wwwroot\\upload";

    public static readonly string[] AllowedExtentions =
        {"txt", "pdf", "doc", "docx", "xls", "xlsx", "png", "jpg", "jpeg", "gif", "csv", "mp3", "mp4"};

    public static bool? FileExists(IFormFile file, double timeDifference, out string fileName)
    {
        fileName = "";
        if (file != null)
        {
            var uploadPath = GetUploadDestination(PrepareFileName(file.FileName), timeDifference);
            fileName = uploadPath;
            var retValue = File.Exists(uploadPath);
            return retValue;
        }

        return null;
    }

    public static async Task<string> Upload(IFormFile file, double timeDifference, string newName = null)
    {
        if (file != null && file.Length > 0 && AllowedExtentions.Contains(GetFileExtension(file.FileName)))
        {
            var uploadPath = GetUploadDestination(PrepareFileName(file.FileName, newName), timeDifference);
            using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                await file.CopyToAsync(stream).ConfigureAwait(false);
            }

            var retValue = GetRelativeUrl(uploadPath);
            return retValue;
        }

        return null;
    }

    public static async Task<FormFile> Download(string filePath, string newName = null)
    {
        if (filePath.HasValue())
        {
            var path = GetPublicUrl(filePath);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            var name = newName ?? GetFileName(filePath);
            return new FormFile(memory, 0, memory.Length, name, name);
        }

        return null;
    }

    private static string GetUploadDestination(string fileName, double timeDifference)
    {
        var yearPath = Path.Combine(BaseFullUploadPath, DateTime.Now.AddMinutes(timeDifference).Year.ToString());
        if (!Directory.Exists(yearPath)) Directory.CreateDirectory(yearPath);

        var monthPath = Path.Combine(yearPath, DateTime.Now.AddMinutes(timeDifference).Month.ToString());
        if (!Directory.Exists(monthPath)) Directory.CreateDirectory(monthPath);

        var dayPath = Path.Combine(monthPath, DateTime.Now.AddMinutes(timeDifference).Day.ToString());
        if (!Directory.Exists(dayPath)) Directory.CreateDirectory(dayPath);

        var retValue = Path.Combine(dayPath,
            $"{GetFileName(fileName)}_{DateTime.Now.AddMinutes(timeDifference).Ticks.ToString().Substring(0, 5)}.{GetFileExtension(fileName)}");
        return retValue;
    }

    public static long? GetFileSize(string filePath)
    {
        if (filePath.HasValue())
            return new FileInfo(GetStaticUrl(filePath)).Length;
        return null;
    }

    public static string GetFileSizeToString(string filePath)
    {
        var size = GetFileSize(filePath);

        if (size.HasValue)
        {
            int iteration;
            for (iteration = 0; iteration < 3 && size > 1024; iteration++) size /= 1024;

            switch (iteration)
            {
                case 0:
                    return $"{size} B";
                case 1:
                    return $"{size} KB";
                case 2:
                    return $"{size} MB";
                case 3:
                    return $"{size} GB";
                default:
                    return $"{size} B";
            }
        }

        return null;
    }

    public static string GetPublicUrl(string filePath)
    {
        if (filePath.HasValue()) return $"/{BaseUploadPath}{filePath.Replace("/", @"\")}";

        return null;
    }

    public static string GetStaticUrl(string filePath)
    {
        if (filePath.HasValue())
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), BaseFullUploadPath);
            return $"{path}{filePath.Replace("/", @"\")}";
        }

        return null;
    }

    public static string GetRelativeUrl(string uploadPath)
    {
        if (uploadPath.HasValue()) return uploadPath.Replace(BaseFullUploadPath, "").Replace(@"\", "/");

        return null;
    }

    private static string PrepareFileName(string fileName, string newName = null)
    {
        if (fileName.HasValue())
        {
            var name = newName ?? GetFileName(fileName);
            var ext = GetFileExtension(fileName);
            var retvalue = $"{name.FixFileName()}.{ext}";
            return retvalue;
        }

        return null;
    }

    public static string GetFileExtension(string fileName)
    {
        if (fileName.HasValue()) return fileName.Split('.').LastOrDefault().ToLower();

        return null;
    }

    public static string GetFileName(string filePath)
    {
        if (filePath.HasValue()) return string.Join(' ', filePath.Split('/').LastOrDefault().Split('.')[..^1]);

        return null;
    }

    public static string FixFileName(this string input)
    {
        return input.Trim().RemoveWhiteSpaces().ToLower()
            .Replace(' ', '-')
            .Replace('%', '-')
            .Replace(':', '-')
            .Replace('~', '-')
            .Replace('/', '-')
            .Replace('!', '-')
            .Replace('@', '-')
            .Replace('#', '-')
            .Replace('$', '-')
            .Replace('^', '-')
            .Replace('&', '-')
            .Replace('*', '-')
            .Replace('(', '-')
            .Replace(')', '-')
            .Replace('+', '-')
            .Replace('=', '-')
            .Replace('[', '-')
            .Replace(']', '-')
            .Replace('{', '-')
            .Replace('}', '-')
            .Replace(',', '-')
            .Replace(';', '-')
            .Replace('`', '-')
            .Replace('<', '-')
            .Replace('>', '-')
            .Replace('|', '-')
            .Replace('/', '-')
            .Replace('\\', '-');
    }

    public static string GetRandomSerialNumber()
    {
        var rndSerialNumber = new Random().Next(100000, 999999).ToString();
        return rndSerialNumber;
    }

    public static async Task UploadHCPicture(IFormFile file, string fileName)
    {
        if (file != null && file.Length > 0 && AllowedExtentions.Contains(GetFileExtension(file.FileName)))
        {
            var dir = Path.Combine(BaseFullUploadPath, "HCPictures");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var uploadPath = Path.Combine(dir, fileName);
            using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                await file.CopyToAsync(stream).ConfigureAwait(false);
            }
        }
    }

    public static string GetHCPictureStaticUrl(int clientId)
    {
        var path = Path.Combine(BaseFullUploadPath, "HCPictures");
        var dir = new DirectoryInfo(path);
        var files = dir.GetFiles();
        foreach (var item in files)
            if (item.Name == clientId + item.Extension)
                return Path.Combine(path, item.Name).Replace(BaseFullUploadPath, BaseUploadPath).Replace(@"\", "/");

        return "";
    }
}