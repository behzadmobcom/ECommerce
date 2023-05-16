namespace ECommerce.ControllersTests.BaseContext
{
    public interface IAuthorizationContextProvider
    {
        AuthorizationContext GetAuthorizationContext();

        /// <summary>
        /// WARNING: This overrides the original claim, so use it with caution.
        /// </summary>
        void RaiseAccessLevel();
        void RestoreAccessLevel();
    }
}
