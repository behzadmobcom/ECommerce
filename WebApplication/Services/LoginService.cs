using WebApplication.Models;
using System.Text;
using Newtonsoft.Json;

namespace WebApplication.Services
{
    public class LoginService : ILoginService
    {
        public async Task<LoginResponse> Authinticate(LoginRequest loginRequest)
        {
            using (var client = new HttpClient())
            {
                string loginRequeststr = JsonConvert.SerializeObject(loginRequest);
                var response = await client.PostAsync("http://94.130.32.183:9090/api/Users/Login",
                    new StringContent(loginRequeststr, Encoding.UTF8, "Application/json"));
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<LoginResponse>(json);
                }
                else
                {
                    return null;
                }
            }

        }

        
    }
}
