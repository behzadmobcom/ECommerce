namespace ECommerce.ControllersTests.BaseContext
{
    public class ForbiddenException : KnownException
    {
        public ForbiddenException(string message)
            : base(message)
        {
        }

        public override string GetErrorCode()
        {
            return KnownErrorCodes.FORBIDDEN;
        }
    }
}
