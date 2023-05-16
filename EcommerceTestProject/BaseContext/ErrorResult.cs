using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ControllersTests.BaseContext
{
    public class ErrorResult
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public object Context { get; set; }
    }
}
