namespace ECommerce.ControllersTests.BaseContext
{
    public interface IRequesterServiceFactory
    {
        IRequesterService Create(string httpClientName);
    }
}
