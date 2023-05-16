namespace ECommerce.ControllersTests.BaseContext
{
    public abstract class KnownException : Exception
    {
        public KnownException(string message)
            : base(message)
        {
        }

        public KnownException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public abstract string GetErrorCode();

        public virtual object GetErrorContextOrDefault()
        {
            return default;
        }
    }
}
