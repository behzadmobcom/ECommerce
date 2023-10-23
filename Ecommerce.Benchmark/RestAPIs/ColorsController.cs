using System.Drawing;
using System.Net.Http.Json;

namespace ECommerce.Benchmark.RestAPIs
{
    public class ColorsController : BaseController
    {
        public async Task<Color> GetAllAsync()
        {
            return await Client.GetFromJsonAsync<Color>($"https://localhost:7257/api/Colors/GetAll");
        }
    }
}
