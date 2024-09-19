using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Commands.QuestionsAndAnswers.CreateQuestionAndAnswers;
using QuizBackend.Application.Commands.QuestionsAndAnswers.DeleteQuestionAndAnswers;
using QuizBackend.Application.Commands.QuestionsAndAnswers.UpdateQuestionAndAnswers;
using QuizBackend.Infrastructure.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace QuizBackend.Api.Controllers;

[Authorize(Roles = "User")]
public class QuestionsAndAnswers : BaseController
{
    private readonly IMediator _mediator;

    public QuestionsAndAnswers(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy = PolicyNames.QuestionOwner)]
    [SwaggerOperation(Summary = "Create Questions with Answers. Returned result is Question Id")]
    public async Task<IActionResult> CreateQuestionAndAnswers(CreateQuestionAndAnswersCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut]
    [Authorize(Policy = PolicyNames.QuestionOwner)]
    [SwaggerOperation(Summary = "Update Questions with Answers")]
    public async Task<IActionResult> UpdateQuestionAndAnswers(UpdateQuestionAndAnswersCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = PolicyNames.QuestionOwner)]
    [SwaggerOperation(Summary = "Delete Question with Answers by Id.")]
    public async Task<IActionResult> DeleteQuestionAndAnswers([FromRoute] Guid id)
    {
        var command = new DeleteQuestionAndAnswersCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}