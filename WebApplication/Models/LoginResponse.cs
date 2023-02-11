namespace WebApplication.Models
{
    public class LoginResponse
    {
        public string ReturnData { get; set; }
        public string Status { get; set; }
        public string Code { get; set; }
        public string Messages { get; set; }
        public string StackTrace { get; set; }
        public string PaginationDetails { get; set; }

    }
}
