using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Commands.QuizzesParticipations.StopQuiz;
using QuizBackend.Application.Commands.QuizzesParticipations.SubmitQuizAnswer;
using QuizBackend.Application.Queries.QuizzesParticipations.GetQuizResult;
using QuizBackend.Application.Commands.QuizzesParticipations.JoinQuiz;
using QuizBackend.Application.Queries.Quizzes.GetQuizParticipation;
using Swashbuckle.AspNetCore.Annotations;
using QuizBackend.Application.Queries.QuizzesParticipations.GetUserAnswer;

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
            result);
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

    [HttpPost("submit")]
    [SwaggerOperation(Summary = "Submit quiz participation, save user answer and calculate score.")]
    public async Task<IActionResult> SubmitQuizParticipation(SubmitQuizAnswerCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{quizParticipationId}/result")]
    [SwaggerOperation(Summary = "Get Quiz result from quizParticipationId")]
    [ProducesResponseType(typeof(QuizResultResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetQuizResult(Guid quizParticipationId)
    {
        var query = new GetQuizResultQuery(quizParticipationId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPatch("{participationId:guid}/stop")]
    public async Task<IActionResult> StopQuiz(Guid participationId)
    {
        var command = new StopQuizCommand(participationId);
        await _mediator.Send(command);

        return NoContent();
    }

    [HttpGet("{participantId}/history")]
    public async Task<IActionResult> GetUserQuizHistory(string participantId)
    {
        var result = await _mediator.Send(new GetUserQuizHistoryQuery(participantId));
        return Ok(result);
    }
}