namespace ECommerce.ControllersTests.BaseContext
{
    public class TokenNotSetException : Exception
    {
        public TokenNotSetException() : base("Token is not set")
        {
        }
    }
}
