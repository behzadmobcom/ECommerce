﻿using Ecommerce.Entities.Helper;
using System;
using System.Net;

namespace Common.Exceptions
{
    public class AppException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public ResultCode ApiStatusCode { get; set; }
        public object AdditionalData { get; set; }

        public AppException()
            : this(ResultCode.Error)
        {
        }

        public AppException(ResultCode statusCode)
            : this(statusCode, null)
        {
        }

        public AppException(string message)
            : this(ResultCode.Error, message)
        {
        }

        public AppException(ResultCode statusCode, string message)
            : this(statusCode, message, HttpStatusCode.InternalServerError)
        {
        }

        public AppException(string message, object additionalData)
            : this(ResultCode.Error, message, additionalData)
        {
        }

        public AppException(ResultCode statusCode, object additionalData)
            : this(statusCode, null, additionalData)
        {
        }

        public AppException(ResultCode statusCode, string message, object additionalData)
            : this(statusCode, message, HttpStatusCode.InternalServerError, additionalData)
        {
        }

        public AppException(ResultCode statusCode, string message, HttpStatusCode httpStatusCode)
            : this(statusCode, message, httpStatusCode, null)
        {
        }

        public AppException(ResultCode statusCode, string message, HttpStatusCode httpStatusCode, object additionalData)
            : this(statusCode, message, httpStatusCode, null, additionalData)
        {
        }

        public AppException(string message, Exception exception)
            : this(ResultCode.Error, message, exception)
        {
        }

        public AppException(string message, Exception exception, object additionalData)
            : this(ResultCode.Error, message, exception, additionalData)
        {
        }

        public AppException(ResultCode statusCode, string message, Exception exception)
            : this(statusCode, message, HttpStatusCode.InternalServerError, exception)
        {
        }

        public AppException(ResultCode statusCode, string message, Exception exception, object additionalData)
            : this(statusCode, message, HttpStatusCode.InternalServerError, exception, additionalData)
        {
        }

        public AppException(ResultCode statusCode, string message, HttpStatusCode httpStatusCode, Exception exception)
            : this(statusCode, message, httpStatusCode, exception, null)
        {
        }

        public AppException(ResultCode statusCode, string message, HttpStatusCode httpStatusCode, Exception exception, object additionalData)
            : base(message, exception)
        {
            ApiStatusCode = statusCode;
            HttpStatusCode = httpStatusCode;
            AdditionalData = additionalData;
        }
    }
}
