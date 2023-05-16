using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ControllersTests.BaseContext
{
    public class UnexpectedHttpErrorException : Exception
    {
        public UnexpectedHttpErrorException(string message) : base(message)
        {
        }
    }
}
