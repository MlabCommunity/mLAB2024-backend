using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace QuizBackend.Api.Controllers;

[SwaggerTag("Temporary - debug only")]
public class DebugController : BaseController
{
    [Authorize]
    [HttpGet("unauthorized")]
    public IActionResult UnauthorizedUser()
    {
        return Ok();
    }

    [Authorize(Roles = "NonExistingRole")]
    [HttpGet("forbidden")]
    public IActionResult ForbiddenUser()
    {
        return Ok();
    }
}