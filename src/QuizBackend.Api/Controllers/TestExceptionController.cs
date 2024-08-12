using Microsoft.AspNetCore.Mvc;
using QuizBackend.Domain.Exceptions;

namespace QuizBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestExceptionController : ControllerBase
    {
        [HttpGet("global")]
        public IActionResult ThrowGlobalException()
        {
            throw new Exception("This is a test exception");
        }

        [HttpGet("badrequest")]
        public IActionResult ThrowBadRequestException()
        {
            throw new BadRequestException("This is a bad request");
        }

        [HttpGet("notfound")]
        public IActionResult ThrowNotFoundException()
        {
            throw new NotFoundException(nameof(User), "1");
        }

        [HttpGet("unauthorized")]
        public IActionResult ThrowUnauthorizedException()
        {
            throw new UnauthorizedException("Unauthorized access");
        }
    }
}
