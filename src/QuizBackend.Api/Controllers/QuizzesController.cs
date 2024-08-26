using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Queries.Quizzes.GetQuiz;
using Swashbuckle.AspNetCore.Annotations;

namespace QuizBackend.Api.Controllers
{
    public class QuizzesController : BaseController
    {
        private readonly IMediator _mediator;

        public QuizzesController(IMediator mediator)
        {
            _mediator = mediator;
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
    }
}
