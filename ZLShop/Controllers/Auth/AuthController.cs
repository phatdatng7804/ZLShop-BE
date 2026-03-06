using Microsoft.AspNetCore.Mvc;
using ZLShop.Services.Interfaces;
using ZLShop.DTOs.Auth;
using ZLShop.Services.Auth;
using ZLShop.Exceptions;

namespace ZLShop.Controllers.Auth{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterRequestDto request)
        {
            try
            {
                var response = await _authService.RegisterAsync(request);
                return Ok(response);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginRequestDto request)
        {
            var response = await _authService.LoginAsync(request);
            if(request != null){
                return Ok(response);
            }
            return BadRequest(new { message = "Login failed" });
        }
    }
}

