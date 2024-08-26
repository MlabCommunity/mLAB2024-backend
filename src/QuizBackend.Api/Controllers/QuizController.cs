using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Commands.Quizzes.CreateQuiz;
using QuizBackend.Application.Commands.Quizzes.GenerateQuiz;
using QuizBackend.Application.Dtos.Quizzes.CreateQuiz;
using QuizBackend.Application.Dtos.Quizzes.GenerateQuiz;

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
            var command = new GenerateQuizCommand(
                quizArguments.Content,
                quizArguments.NumberOfQuestions,
                quizArguments.TypeOfQuestions
            );

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("create-quiz")]
        [ProducesResponseType(typeof(CreateQuizDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateQuiz(CreateQuizDto createQuizDto)
        {
            //TODO Validator to createQuizDto
            var command = new CreateQuizCommand(createQuizDto);

            var quizId = await _mediator.Send(command);

            return Ok(quizId);
        }
    }
}
