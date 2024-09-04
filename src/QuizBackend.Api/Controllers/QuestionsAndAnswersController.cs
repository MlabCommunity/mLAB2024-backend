using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Commands.QuestionsAndAnswers.CreateQuestionAndAnswers;
using QuizBackend.Application.Commands.QuestionsAndAnswers.DeleteQuestionAndAnswers;
using QuizBackend.Application.Commands.QuestionsAndAnswers.UpdateQuestionAndAnswers;
using Swashbuckle.AspNetCore.Annotations;

namespace QuizBackend.Api.Controllers;

[Authorize]
public class QuestionsAndAnswersController : BaseController
{
    private readonly IMediator _mediator;

    public QuestionsAndAnswersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create-question")]
    [SwaggerOperation(Summary = "Create Questions with Answers. Returned result is Question Id")]
    public async Task<IActionResult> CreateQuestionAndAnswers(CreateQuestionAndAnswersCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("update-question")]
    [SwaggerOperation(Summary = "Update Questions with Answers. Returned result is Question Id")]
    public async Task<IActionResult> UpdateQuestionAndAnswers(UpdateQuestionAndAnswersCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete Question with Answers by Id.")]
    public async Task<IActionResult> DeleteQuestionAndAnswers(Guid id)
    {
        var command = new DeleteQuestionAndAnswersCommand(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}