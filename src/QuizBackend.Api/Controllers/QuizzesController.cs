using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Commands.Quizzes.CreateQuiz;
using QuizBackend.Application.Commands.Quizzes.DeleteQuiz;
using QuizBackend.Application.Commands.Quizzes.GenerateQuiz;
using QuizBackend.Application.Commands.Quizzes.RegenerateQuiz;
using QuizBackend.Application.Commands.Quizzes.UpdateAvailability;
using QuizBackend.Application.Commands.Quizzes.UpdateQuiz;
using QuizBackend.Application.Commands.Quizzes.UpdateStatusQuiz;
using QuizBackend.Application.Commands.UpdateStatusQuiz;
using QuizBackend.Application.Dtos.Paged;
using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Queries.Quizzes.GetQuiz;
using QuizBackend.Application.Queries.Quizzes.GetQuizzes;
using QuizBackend.Domain.Enums;
using QuizBackend.Infrastructure.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace QuizBackend.Api.Controllers;

[Authorize(Policy = PolicyNames.User)]
public class QuizzesController : BaseController
{
    private readonly IMediator _mediator;

    public QuizzesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("generate-quiz")]
    [SwaggerOperation(Summary = "Generating Quiz with questions and anserws", Description = "QuestionType: SingleChoice, TrueFalse")]
    [ProducesResponseType(typeof(GenerateQuizResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GenerateQuizFromPromptTemplateAsync([FromForm] GenerateQuizCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{Id}")]
    [SwaggerOperation(
        Summary = "Retrieves a quiz by its unique Id.",
        Description = "Fetches the details of a quiz specified by its unique Id.")]
    [ProducesResponseType(typeof(QuizDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<QuizDetailsDto>> GetQuiz([FromRoute] Guid Id, CancellationToken cancellationToken)
    {
        var query = new GetQuizQuery(Id);
        var quiz = await _mediator.Send(query);

        return Ok(quiz);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Retrieves a paginated list of quizzes.",
        Description = "Fetches a list of quizzes based on pagination.")]
    [ProducesResponseType(typeof(PagedDto<QuizDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PagedDto<QuizDto>>> GetPagedQuizzes([FromQuery] GetPagedQuizzesQuery query, CancellationToken cancellation)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a new quiz.",
        Description = "Create a new quiz based on generated quiz")]
    [ProducesResponseType(typeof(CreateQuizResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateQuiz(CreateQuizCommand command)
    {
        var quizId = await _mediator.Send(command);
        return Ok(quizId);
    }

    [HttpPatch("{id}/status")]
    [SwaggerOperation(
        Summary = "Update the status of a quiz",
        Description = "Updates the status of a quiz based on its ID. The status can be 'Active' or 'Inactive'."
     )]
    [ProducesResponseType(typeof(UpdateQuizStatusResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateQuizStatusResponse>> UpdateQuizStatus([FromRoute] Guid id, [FromBody] Status status)
    {
        var command = new UpdateStatusQuizCommand(id, status);
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPatch("{id}/availability")]
    [SwaggerOperation(
       Summary = "Update the availability of a quiz",
       Description = "Updates the availability of a quiz based on its ID. The status can be 'Public' or 'Private'."
    )]
    [ProducesResponseType(typeof(UpdateAvailabilityResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateQuizStatusResponse>> UpdateQuizAvailability([FromRoute] Guid id, [FromBody] Availability availability)
    {
        var command = new UpdateAvailabilityCommand(id, availability);
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
       Summary = "Delete a quiz",
       Description = "Deletes a quiz based on its ID."
    )]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteQuiz([FromRoute] Guid id)
    {
        var command = new DeleteQuizCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update quiz title and description. In future this function will be update status and availability")]
    public async Task<IActionResult> UpdateQuiz(UpdateQuizCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("regenerate-quiz")]
    [SwaggerOperation(Summary = "Regenerating Quiz with questions and anserws")]
    [ProducesResponseType(typeof(GenerateQuizResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> RegenerateQuizFromPromptTemplateAsync()
    {
        var command = new RegenerateQuizCommand();
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}