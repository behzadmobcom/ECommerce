using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.ViewModel;
public class SmsIr
{
    public int Status { get; set; }
    public string Message { get; set; }
    public DataResponseBody Data { get; set; }
 
}
public class DataResponseBody
{
    public int MessageId { get; set; }
    public decimal Cost { get; set; }
}
