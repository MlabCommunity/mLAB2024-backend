using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Commands.GenerateQuiz;
using QuizBackend.Application.Dtos.CreateQuiz;
using QuizBackend.Application.Dtos.Quiz;

namespace QuizBackend.Api.Controllers
{
    public class QuizController : BaseController
    {
        private readonly IMediator _mediator;

        public QuizController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("generate-quiz")]
        [ProducesResponseType(typeof(GenerateQuizDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GenerateQuizFromPromptTemplateAsync(QuizArgumentsDto quizArguments)
        {
            if (quizArguments == null)
            {
                return BadRequest();
            }

            var command = new GenerateQuizCommand(
                quizArguments.Content,
                quizArguments.NumberOfQuestions,
                quizArguments.TypeOfQuestions
            );

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
