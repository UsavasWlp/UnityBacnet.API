using Microsoft.AspNetCore.Mvc;
using UnityBacnet.API.Models;
using UnityBacnet.API.Services;

namespace UnityBacnet.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UnityController : Controller
    {
        private readonly UnityApiService _api;
        public UnityController(UnityApiService api)
        {
            _api = api;
        }

        [HttpPost("send-test")]
        public async Task<IActionResult> SendTest()
        {
            var sample = new UnityAssetReading
            {
                AssetId = 1001,
                AssetType = "HVAC",
                Value = 23.5,
                ReadingType = "Temperature",
                HasAlarm = false
            };

            await _api.SendReadingAsync(sample);

            return Ok("Sent");
        }
    }
}
