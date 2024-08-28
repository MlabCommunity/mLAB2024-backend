using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Dtos.Paged;
using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Queries.Quizzes.GetQuiz;
using QuizBackend.Application.Queries.Quizzes.GetQuizzes;
using Swashbuckle.AspNetCore.Annotations;
using QuizBackend.Application.Commands.Quizzes.CreateQuiz;
using QuizBackend.Application.Commands.Quizzes.GenerateQuiz;
using QuizBackend.Application.Dtos.Quizzes.GenerateQuiz;
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
        [SwaggerOperation(Summary = "Generating Quiz with questions and anserws", Description = "QuestionType: MultipleChoices = 0, TrueFalse = 1")]
        [ProducesResponseType(typeof(GenerateQuizDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GenerateQuizFromPromptTemplateAsync(GenerateQuizCommand command)
        {
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
