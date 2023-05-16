using System.Net.Http.Headers;

namespace ECommerce.ControllersTests.BaseContext
{
    public class MachineAuthenticationDelegatingHandler : DelegatingHandler
    {
        private readonly IPlatformMachineAuthenticationService _platformMachineAuthenticationService;

        public MachineAuthenticationDelegatingHandler(
            IPlatformMachineAuthenticationService platformMachineAuthenticationService)
        {
            _platformMachineAuthenticationService = platformMachineAuthenticationService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            MachineAccessToken machineAccessToken = await _platformMachineAuthenticationService.GetAccessTokenAsync();
            request.Headers.Authorization = new AuthenticationHeaderValue(
                machineAccessToken.TokenType,
                machineAccessToken.Token);
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
}
