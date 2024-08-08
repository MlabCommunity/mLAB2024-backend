using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Dtos;
using QuizBackend.Application.Services;

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

        [HttpPost("signup")]
        public async Task<ActionResult> SignUp(RegisterRequestDto request)
        {
            var (IsSuceed, UserId) = await _authService.SignUp(request);

            if (!IsSuceed)
            {
                return BadRequest("Error in signUp");
            }

            return Ok(new { message = "User has been created", id = UserId } );

        }
    }
}
