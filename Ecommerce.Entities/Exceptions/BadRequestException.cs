using Ecommerce.Entities.Helper;
using System;

namespace Common.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException()
            : base(ResultCode.BadRequest)
        {
        }

        public BadRequestException(string message)
            : base(ResultCode.BadRequest, message)
        {
        }

        public BadRequestException(object additionalData)
            : base(ResultCode.BadRequest, additionalData)
        {
        }

        public BadRequestException(string message, object additionalData)
            : base(ResultCode.BadRequest, message, additionalData)
        {
        }

        public BadRequestException(string message, Exception exception)
            : base(ResultCode.BadRequest, message, exception)
        {
        }

        public BadRequestException(string message, Exception exception, object additionalData)
            : base(ResultCode.BadRequest, message, exception, additionalData)
        {
        }
    }
}
