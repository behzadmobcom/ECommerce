using System.Net.Http.Headers;

namespace ECommerce.ControllersTests.BaseContext
{
    public static class HttpRequestHeadersExtensions
    {
        public static void AddOrUpdate(
            this HttpRequestHeaders httpRequestHeaders,
            string headerName,
            string headerValue)
        {
            if (httpRequestHeaders.Contains(headerName))
            {
                httpRequestHeaders.Remove(headerName);
            }
            httpRequestHeaders.Add(headerName, headerValue);
        }
    }
}
