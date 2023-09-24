using Ecommerce.Entities.Helper;
using System;

namespace Common.Exceptions
{
    public class LogicException : AppException
    {
        public LogicException() 
            : base(ResultCode.DatabaseError)
        {
        }

        public LogicException(string message) 
            : base(ResultCode.DatabaseError, message)
        {
        }

        public LogicException(object additionalData) 
            : base(ResultCode.DatabaseError, additionalData)
        {
        }

        public LogicException(string message, object additionalData) 
            : base(ResultCode.DatabaseError, message, additionalData)
        {
        }

        public LogicException(string message, Exception exception)
            : base(ResultCode.DatabaseError, message, exception)
        {
        }

        public LogicException(string message, Exception exception, object additionalData)
            : base(ResultCode.DatabaseError, message, exception, additionalData)
        {
        }
    }
}
