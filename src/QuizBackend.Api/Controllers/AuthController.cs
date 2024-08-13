using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Dtos;
using QuizBackend.Application.Interfaces;
using System.Security;
using System.Security.Authentication;
using System.Security.Claims;

namespace QuizBackend.Api.Controllers
{
    [ApiController]
    [Route("/api")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(LoginDto loginDto)
        {
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
            var signUpResponse = await _authService.SignUp(request);

            if (!signUpResponse.Succeed)
            {
                return BadRequest("Error in signUp");
            }
            return Ok(signUpResponse);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
           var result = await _authService.LogoutAsync();
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto refreshTokenRequest)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }
            try
            {
                var jwtAuthResult = await _authService.RefreshTokenAsync(refreshTokenRequest.RefreshToken, userId);
                return Ok(jwtAuthResult);
            }
            catch(SecurityException ex) 
            {
                return Unauthorized(new { ex.Message });
            }
        }
    }
}
