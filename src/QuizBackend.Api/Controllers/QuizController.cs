using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Commands.Quizzes.CreateQuiz;
using QuizBackend.Application.Commands.Quizzes.GenerateQuiz;
using QuizBackend.Application.Dtos.Quizzes.GenerateQuiz;
using Swashbuckle.AspNetCore.Annotations;
using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Queries.Quizzes.GetQuiz;
using Microsoft.AspNetCore.Authorization;

namespace QuizBackend.Api.Controllers
{
    public class QuizController : BaseController
    {
        private readonly IMediator _mediator;

        public QuizController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("generate-quiz")]
        [SwaggerOperation(Summary = "Generating Quiz with questions and anserws", Description = "typeOfQuestions parameter: 'multiple choices' or 'true/false'")]
        [ProducesResponseType(typeof(GenerateQuizDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GenerateQuizFromPromptTemplateAsync(QuizArgumentsDto quizArguments)
        {
            var command = new GenerateQuizCommand(
                quizArguments.Content,
                quizArguments.NumberOfQuestions,
                quizArguments.TypeOfQuestions
            );

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [Authorize]
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

        [Authorize]
        [HttpPost("create-quiz")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateQuiz(CreateQuizCommand command)
        {
            var quizId = await _mediator.Send(command);

            return Ok(quizId);
        }
    }
}
