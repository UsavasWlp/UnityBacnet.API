using Microsoft.Extensions.Options;
using UnityBacnet.API.Models;

namespace UnityBacnet.API.Infrastructure.Auth
{
    public class UnityAuthService
    {

        private readonly HttpClient _httpClient;
        private readonly UnityApiSettings _settings;
        private string _token;
        private DateTime _expires;

        public UnityAuthService(HttpClient httpClient,IOptions<UnityApiSettings> options)
        {
            _httpClient = httpClient;
            _settings = options.Value;
        }

        public async Task<string> GetTokenAsync(bool forceRefresh = false)
        {
            //token expire check
            if (!forceRefresh && !string.IsNullOrEmpty(_token) && DateTime.UtcNow < _expires)
            {
                return _token;
            }

            var request = new
            {
                username = _settings.Username,
                password = _settings.Password
            };

            var response = await _httpClient.PostAsJsonAsync(
                "services/General/Authentication/GetToken",
                request
            );

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<UnityTokenResponse>();

            _token = result.Token;
            _expires = result.Expires;

            return _token;
        }

    }
}
