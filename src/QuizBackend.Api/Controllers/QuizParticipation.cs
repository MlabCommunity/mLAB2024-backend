using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Queries.Quizzes.JoinQuiz;
using Swashbuckle.AspNetCore.Annotations;

namespace QuizBackend.Api.Controllers;

public class QuizParticipation : BaseController
{
    private readonly IMediator _mediator;

    public QuizParticipation(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/api/{code}")]
    [SwaggerOperation(
        Summary = "Joins a user to a quiz.",
        Description = "Allows a user to join a quiz by providing the quiz code. Returns the quiz details if successful.")]
    [ProducesResponseType(typeof(JoinQuizResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> JoinToQuiz([FromRoute] string code)
    {
        var command = new JoinQuizCommand(code);
        var quiz = await _mediator.Send(command);
        return Ok(quiz);
    }
}