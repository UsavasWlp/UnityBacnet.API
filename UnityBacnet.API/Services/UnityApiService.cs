using UnityBacnet.API.Models;

namespace UnityBacnet.API.Services
{
    public class UnityApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UnityApiService> _logger;

        public UnityApiService(HttpClient httpClient, ILogger<UnityApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task SendReadingAsync(UnityAssetReading reading)
        {
            //w error logging
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    "services/AMS/Readings/AddReading", //example endpoint
                    reading
                );
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending reading for AssetId {AssetId}", reading.AssetId);
                throw;
            }
        }
    }
}
