using CommunityControl.Api.Dtos.Auth;
using CommunityControl.Api.Services;
using CommunityControl.API.Dtos.Auth;
using CommunityControl.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommunityControl.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _authService.ValidateUserAsync(request.Email, request.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            var token = _authService.GenerateToken(user);

            var response = new LoginResponse
            {
                Token = token,
                ExpiresInMinutes = 60, // también puedes leerlo de config
                Email = user.Email,
                Role = user.Role
            };

            return Ok(response);
        }
    }
}
