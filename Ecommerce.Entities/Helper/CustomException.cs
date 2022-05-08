using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Helper
{
    public class CustomException : Exception
    {
        public CustomException(CustomHttpStatus customHttpStatus)
        {
            Message = customHttpStatus.GetDescription();
            Status = (int)customHttpStatus;
        }

        public CustomException(HttpStatusCode httpStatusCode)
        {
            Message = httpStatusCode.GetDescription();
            Status = (int)httpStatusCode;
        }

        public override string Message { get; }
        public int Status { get; set; }
    }
    public static class EnumerationExtension
    {
        public static string GetDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
