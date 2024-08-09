using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Dtos;
using QuizBackend.Application.Interfaces;
using System.Security.Authentication;

namespace QuizBackend.Api.Controllers
{
    [Route("api/")]
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
            try
            {
                var response = await _authService.SignUpAsync(request);

                return Ok(response); 
            }
            catch (ValidationException ex)
            {
                if (ex.Message.Contains("User already exists."))
                {
                    return Conflict(ex.Message);
                   
                }
                return BadRequest(ex.Message);   
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Ok();
        }
    }
}
