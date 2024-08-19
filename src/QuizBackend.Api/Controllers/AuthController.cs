using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Dtos;
using QuizBackend.Application.Interfaces;
using System.Security;
using System.Security.Claims;

namespace QuizBackend.Api.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(LoginDto loginDto)
        {
            var jwtAuthResult = await _authService.LoginAsync(loginDto);
            return Ok(jwtAuthResult);
        }

        [HttpPost("signup")]
        public async Task<ActionResult> SignUp(RegisterRequestDto request)
        {
            var response = await _authService.SignUpAsync(request);
            return Ok(response);
         
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
