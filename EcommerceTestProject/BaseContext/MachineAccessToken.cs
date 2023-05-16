namespace ECommerce.ControllersTests.BaseContext
{
    public class MachineAccessToken
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string Audience { get; set; }
        public string TokenType { get; set; }
    }
}
