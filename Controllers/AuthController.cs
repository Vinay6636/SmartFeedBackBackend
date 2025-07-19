using Microsoft.AspNetCore.Mvc;
using SmartFeedBack.Dtos.Auth;
using SmartFeedBack.Services;

namespace SmartFeedBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;
        public AuthController(AuthService service) => _service = service;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
            => Ok(await _service.RegisterAsync(dto));

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
            => Ok(await _service.LoginAsync(dto));
    }
}
