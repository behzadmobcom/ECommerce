using System.Net.Http.Headers;

namespace ECommerce.Benchmark.RestAPIs
{
    public class BaseController
    {
        public static readonly HttpClient Client = new();

        public BaseController()
        {
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
