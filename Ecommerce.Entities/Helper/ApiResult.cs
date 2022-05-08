using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace Entities.Helper
{
    public class CookieData : ActionResult
    {
        public string Key { get; set; }
        public int Value { get; set; }
        public int Days { get; set; }

        public CookieData(string key, int value, int days = 30)
        {
            Key = key;
            Value = value;
            Days = days;
        }
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
        public PaginationDetails? PaginationDetails { get; set; }
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
        Success = 0,
        ServerDontResponse = 10,
        Error = 1,
        Repetitive = 3,
        NotFound = 4,
        DatabaseError = 5,
        BadRequest = 6,
        DeActive = 7
    }

    public enum ServiceCode
    {
        Success = 0,
        Error = 1,
        Info = 2,
        Warning = 3
    }
}
