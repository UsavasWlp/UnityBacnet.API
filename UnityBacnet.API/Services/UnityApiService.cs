using UnityBacnet.API.Models;

namespace UnityBacnet.API.Services
{
    public class UnityApiService
    {
        private readonly HttpClient _httpClient;

        public UnityApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendReadingAsync(UnityAssetReading reading)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "services/AMS/Readings/AddReading", // example endpoint
                reading
            );

            response.EnsureSuccessStatusCode();
        }
    }
}
