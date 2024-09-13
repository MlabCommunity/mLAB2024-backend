using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Commands.QuizzesParticipations.StopQuiz;
using QuizBackend.Application.Commands.QuizzesParticipations.SubmitQuizAnswer;
using QuizBackend.Application.Queries.QuizzesParticipations.GetQuizResult;
using Swashbuckle.AspNetCore.Annotations;

namespace QuizBackend.Api.Controllers;

public class ParticipationsController : BaseController
{
    private readonly IMediator _mediator;

    public ParticipationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("submit")]
    [SwaggerOperation(Summary = "Submit quiz participation, save user answer and calculate score.")]
    public async Task<IActionResult> SubmitQuizParticipation(SubmitQuizAnswerCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{quizParticipationId}")]
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
}