using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Commands.QuizzesParticipations.SubmitQuizAnswer;
using QuizBackend.Application.Queries.QuizzesParticipations.GetQuizResult;
using Swashbuckle.AspNetCore.Annotations;

namespace QuizBackend.Api.Controllers;

public class QuizParticipationController : BaseController
{
    private readonly IMediator _mediator;

    public QuizParticipationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("submit-quiz-participation")]
    [SwaggerOperation(Summary = "Submit quiz participation, save user answer and calculate score.")]
    public async Task<IActionResult> SubmitQuizParticipation(SubmitQuizAnswerCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{quizParticipationId}")]
    [SwaggerOperation(Summary = "Get Quiz result from quizParticipationId")]
    public async Task<IActionResult> GetQuizResult(Guid quizParticipationId)
    {
        var query = new GetQuizResultQuery(quizParticipationId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}