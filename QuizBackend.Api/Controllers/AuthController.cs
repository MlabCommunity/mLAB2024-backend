using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Dtos;
using QuizBackend.Application.Interfaces;

namespace QuizBackend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var jwtAuthResult = await _authService.LoginAsync(loginDto);
                return Ok(jwtAuthResult);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { ex.Message });
            }
        }

        [HttpPost("signup")]
        public async Task<ActionResult> SignUp(RegisterRequestDto request)
        {
            var (IsSuceed, UserId) = await _authService.SignUp(request);

            if (!IsSuceed)
            {
                return BadRequest("Error in signUp");
            }

            return Ok(new { message = "User has been created", id = UserId });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Ok();
        }
    }
}
