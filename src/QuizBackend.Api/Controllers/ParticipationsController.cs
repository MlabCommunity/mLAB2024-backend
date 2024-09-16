using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Commands.QuizzesParticipations.JoinQuiz;
using QuizBackend.Application.Queries.Quizzes.GetQuizParticipation;
using Swashbuckle.AspNetCore.Annotations;

namespace QuizBackend.Api.Controllers;

[Authorize]
public class ParticipationsController : BaseController
{
    private readonly IMediator _mediator;

    public ParticipationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("/api/{joinCode}")]
    [SwaggerOperation(
        Summary = "Register participation",
        Description = "Registers authenticated user participation by join code quiz."
    )]
    [ProducesResponseType(typeof(JoinQuizResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> RegisterParticipation([FromRoute] string joinCode)
    {
        var result = await _mediator.Send(new JoinQuizCommand(joinCode));

        return CreatedAtAction(
            nameof(GetQuizParticipation),
            new { result.Id },
            null);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
         Summary = "Get quiz participation",
         Description = "Retrieves the details of a specific quiz participation."
     )]
    [ProducesResponseType(typeof(QuizParticipationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> GetQuizParticipation([FromRoute] Guid id)
    {
        var query = new GetQuizParticipationQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
