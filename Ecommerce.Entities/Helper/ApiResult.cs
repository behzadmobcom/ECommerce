using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Entities.Helper;

public class CookieData : ActionResult
{
    public CookieData(string key, int value, int days = 30)
    {
        Key = key;
        Value = value;
        Days = days;
    }

    public string Key { get; set; }
    public int Value { get; set; }
    public int Days { get; set; }
}

public class ServiceResult : ActionResult
{
    public PaginationDetails PaginationDetails { get; set; }
    public ServiceCode Code { get; set; }
    public string? Message { get; set; }
}

public class ServiceResult<T> : ActionResult
{
    public int Status { get; set; } = 200;
    public string? StackTrace { get; set; }
    public PaginationDetails PaginationDetails { get; set; }
    public ServiceCode Code { get; set; }
    public string? Message { get; set; }
    public T ReturnData { get; set; }
}

public class ApiResult : ActionResult
{
    public int Status { get; set; } = 200;
    public string? StackTrace { get; set; }
    public PaginationDetails? PaginationDetails { get; set; }
    public ResultCode Code { get; set; }
    public IEnumerable<string>? Messages { get; set; }
    public object? ReturnData { get; set; }

    public string? GetBody()
    {
        return Messages?.Aggregate("", (current, message) => current + message + Environment.NewLine);
    }
}

public class ApiResult<T>
{
    public int Status { get; set; } = 200;
    public string? StackTrace { get; set; }
    public PaginationDetails? PaginationDetails { get; set; }
    public ResultCode Code { get; set; }
    public IEnumerable<string>? Messages { get; set; }
    public T ReturnData { get; set; }

    public string? GetBody()
    {
        return Messages?.Aggregate("", (current, message) => current + message + Environment.NewLine);
    }
}

public enum ResultCode
{
    [Display(Name = "عملیات با موفقیت انجام شد")]
    Success = 0,

    [Display(Name = "سرور در دسترس نمی باشد")]
    ServerDontResponse = 10,

    [Display(Name = "خطایی در سرور رخ داده است")]
    Error = 1,

    [Display(Name = "اطلاعات تکراری می باشد")]
    Repetitive = 3,

    [Display(Name = "یافت نشد")]
    NotFound = 4,

    [Display(Name = "خطایی در پردازش رخ داد ")]
    DatabaseError = 5,

    [Display(Name = "پارامتر های ارسالی معتبر نیستند")]
    BadRequest = 6,

    [Display(Name = "غیر فعال می باشد")]
    DeActive = 7,

    [Display(Name = "اطلاعات خواسته شده وجود ندارد")]
    NotExist = 8,

    [Display(Name = "خطای احراز هویت")]
    UnAuthorized = 6
}

public enum ServiceCode
{
    Success = 0,
    Error = 1,
    Info = 2,
    Warning = 3
}