using Microsoft.AspNetCore.Mvc;
using UnityBacnet.API.Models;
using UnityBacnet.API.Models.DTOs;
using UnityBacnet.API.Services;

namespace UnityBacnet.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UnityController : Controller
    {
        private readonly UnityApiService _api;

        [HttpPost("send")]
        public async Task<IActionResult> Send(SendReadingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reading = new UnityAssetReading
            {
                AssetId = request.AssetId,
                AssetType = request.AssetType,
                Value = request.Value,
                ReadingType = request.ReadingType,
                HasAlarm = request.HasAlarm
            };

            await _api.SendReadingAsync(reading);

            return Ok(new UnityApiResponse
            {
                Success = true,
                Message = "Reading sent successfully"
            });
        }
    }
}
