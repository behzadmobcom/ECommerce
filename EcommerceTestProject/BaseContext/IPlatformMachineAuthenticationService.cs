namespace ECommerce.ControllersTests.BaseContext
{
    public interface IPlatformMachineAuthenticationService
    {
        Task<MachineAccessToken> GetAccessTokenAsync();
    }
}
