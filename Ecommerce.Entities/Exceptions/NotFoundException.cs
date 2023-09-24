using Ecommerce.Entities.Helper;
using System;

namespace Common.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException()
            : base(ResultCode.NotFound)
        {
        }

        public NotFoundException(string message)
            : base(ResultCode.NotFound, message)
        {
        }

        public NotFoundException(object additionalData)
            : base(ResultCode.NotFound, additionalData)
        {
        }

        public NotFoundException(string message, object additionalData)
            : base(ResultCode.NotFound, message, additionalData)
        {
        }

        public NotFoundException(string message, Exception exception)
            : base(ResultCode.NotFound, message, exception)
        {
        }

        public NotFoundException(string message, Exception exception, object additionalData)
            : base(ResultCode.NotFound, message, exception, additionalData)
        {
        }
    }
}
