using System.Net;
using System.Net.Http.Headers;
using UnityBacnet.API.Infrastructure.Auth;

namespace UnityBacnet.API.Infrastructure.Http
{
    public class UnityAuthHandler : DelegatingHandler
    {
        private readonly UnityAuthService _authService;
        private readonly ILogger<UnityAuthHandler> _logger;

        public UnityAuthHandler(UnityAuthService authService, ILogger<UnityAuthHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
     HttpRequestMessage request,
     CancellationToken cancellationToken)
        {
            var token = await _authService.GetTokenAsync("test", "test");

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await base.SendAsync(request, cancellationToken);

            // Catching 401 error
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _logger.LogWarning("Unauthorized response received. Attempting token refresh...");

                // Get new token
                var newToken = await _authService.GetTokenAsync("test", "test", true);

                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", newToken);

                // send againg to request token
                response = await base.SendAsync(request, cancellationToken);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _logger.LogError("Unauthorized after token refresh. Url: {Url}, Method: {Method}",request.RequestUri,request.Method);

                    throw new Exception("Unauthorized after token refresh");
                }
            }

            return response;
        }
    }
}
