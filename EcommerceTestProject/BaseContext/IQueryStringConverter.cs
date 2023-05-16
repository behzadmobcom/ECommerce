namespace ECommerce.ControllersTests.BaseContext
{
    public interface IQueryStringConverter
    {
        string ConvertToQueryString<T>(T queryParams)
            where T : class;
    }
}
