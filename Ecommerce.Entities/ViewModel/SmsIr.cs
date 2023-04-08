namespace Ecommerce.Entities.ViewModel;
public class ResponseVerifySmsIrViewModel
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

public class RequestVerifySmsIrViewModel
{
    public string Mobile { get; set; }
    public int TemplateId { get; set; }
    public RequestVerifySmsIrParameters[] Parameters { get; set; }

}
public class RequestVerifySmsIrParameters
{
    public string Name { get; set; }
    public string Value { get; set; }
}



 