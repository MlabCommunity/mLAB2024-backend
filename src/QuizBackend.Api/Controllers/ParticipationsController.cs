using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Commands.Quizzes.JoinQuiz;
using Swashbuckle.AspNetCore.Annotations;

namespace QuizBackend.Api.Controllers;

public class ParticipationsController : BaseController
{
    private readonly IMediator _mediator;

    public ParticipationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("/api/{code}")]
    [SwaggerOperation(
        Summary = "Join a quiz",
        Description = "Joins a quiz using the provided join code. Creates a guest account for unauthenticated users."
    )]
    [ProducesResponseType(typeof(JoinQuizResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> JoinToQuiz([FromRoute] string code, [FromBody] string username)
    {
        var command = new JoinQuizCommand(code, username);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
