using System.Net.Http.Headers;
using UnityBacnet.API.Infrastructure.Auth;

namespace UnityBacnet.API.Infrastructure.Http
{
    public class UnityAuthHandler:  DelegatingHandler
    {
        private readonly UnityAuthService _authService;

        public UnityAuthHandler(UnityAuthService authService)
        {
            _authService = authService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // TODO: We will retrieve the username/password from the config (placeholder for now).
            var token = await _authService.GetTokenAsync("test", "test");

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
