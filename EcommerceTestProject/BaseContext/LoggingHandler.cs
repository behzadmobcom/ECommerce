using Microsoft.Extensions.Logging;

namespace ECommerce.ControllersTests.BaseContext
{
    public class LoggingHandler : DelegatingHandler
    {
        private readonly ILogger _logger;

        public LoggingHandler(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LoggingHandler>();
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;
            try
            {
                _logger.LogDebug("Request: {Request}", request);
                if (request.Content is not null)
                {
                    _logger.LogDebug("Request Content: {ReadAsStringAsync}", await request.Content.ReadAsStringAsync(cancellationToken));
                }
                response = await base.SendAsync(request, cancellationToken);
                _logger.LogDebug("Response: {Response}", response);
                if (((HttpContent?)response.Content) != null)
                {
                    _logger.LogDebug("Request Content: {ReadAsStringAsync}", await response.Content.ReadAsStringAsync(cancellationToken));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in SendAsync");
                throw;
            }
            return response;
        }
    }
}
