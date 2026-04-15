using Microsoft.AspNetCore.Mvc;
using UnityBacnet.API.Infrastructure.Auth;

namespace UnityBacnet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UnityAuthService _authService;

        public AuthController(UnityAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("token")]
        public async Task<IActionResult> GetToken()
        {
            var token = await _authService.GetTokenAsync();
            return Ok(token);
        }
    }
}
