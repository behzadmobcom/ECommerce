using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ECommerce.ControllersTests.BaseContext
{
    public class RequesterServiceUserClient : RequesterService, IRequesterService
    {
        private string _token;

        public RequesterServiceUserClient(HttpClient httpClient)
            : base(httpClient)
        {
        }

        public override Task<HttpRequestMessage> GetHttpRequestMessageAsync(HttpMethod httpMethod, string partialUrl)
        {
            HttpRequestMessage request = new(httpMethod, partialUrl);
            request = SetAuthorizationHeader(request);
            request.Version = HttpVersion.Version11;
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("MediaTypeNameConstants.BSON"));
            return Task.FromResult(request);
        }

        private HttpRequestMessage SetAuthorizationHeader(HttpRequestMessage request)
        {
            ValidateAccessToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            return request;
        }

        public override void SetToken(string token)
        {
            _token = token;
        }

        private void ValidateAccessToken()
        {
            if (string.IsNullOrWhiteSpace(_token))
            {
                throw new TokenNotSetException();
            }
        }

        public override IRequesterService WithAuthorizationHeaders(AuthorizationHeaders authorizationHeaders)
        {
            throw new NotSupportedException("This method is not supported in a client to machine request.");
        }
    }
}
