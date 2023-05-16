namespace ECommerce.ControllersTests.BaseContext
{
    public class BadRequestException : Exception
    {
        public ErrorResult ErrorResult { get; private set; }

        public BadRequestException(string message, ErrorResult errorResult)
            : base(message)
        {
            ErrorResult = errorResult;
        }
    }
}
