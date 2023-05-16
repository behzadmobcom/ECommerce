namespace ECommerce.ControllersTests.BaseContext
{
    public class UnauthorizedException : KnownException
    {
        public UnauthorizedException(string message) : base(message)
        {
        }

        public override string GetErrorCode()
        {
            return KnownErrorCodes.UNAUTHORIZED;
        }
    }
}
