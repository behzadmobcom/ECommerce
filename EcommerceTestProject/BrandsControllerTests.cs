using RichardSzalay.MockHttp;
using Xunit;

namespace ECommerce.ControllersTests
{
    public class BrandsControllerTests
    {
        private readonly HttpClient _http;

        public BrandsControllerTests()
        {
            var mockHttp = new MockHttpMessageHandler();
            _http = new HttpClient(mockHttp);
        }


        [Fact]
        public async Task GetAll_GetAllBrands_Return2Brands()
        {
            var response = await _http.GetAsync("http://89.144.174.200:8080/api/Brands/GetAll");
        }

    }
}
