using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Dtos;
using QuizBackend.Application.Interfaces;
using System.Security.Authentication;

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
        public async Task<IActionResult> SignIn([FromQuery] LoginDto loginDto)
        {
          
            var jwtAuthResult = await _authService.LoginAsync(loginDto);
            return Ok(jwtAuthResult);
           
        }

        [HttpPost("signup")]
        public async Task<ActionResult> SignUp(RegisterRequestDto request)
        {
            var response = await _authService.SignUpAsync(request);
            return Ok(new { message = "User created successfully", id = response.UserId});  
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Ok();
        }
    }
}
