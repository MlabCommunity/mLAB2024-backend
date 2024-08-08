using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Dtos;
using QuizBackend.Application.Interfaces;
using System.Security.Authentication;

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

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromQuery] LoginDto loginDto)
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
            catch (InvalidCredentialException ex)
            {
                return Unauthorized(new { ex.Message });
            }
        }

        [HttpPost("signup")]
        public async Task<ActionResult> SignUp(RegisterRequestDto request)
        {
            var response = await _authService.SignUp(request);

            if (!response.Succeed)
            {
                return BadRequest("Error in signUp");
            }

            return Ok(new { message = "User has been created", id = response.UserId });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Ok();
        }
    }
}
