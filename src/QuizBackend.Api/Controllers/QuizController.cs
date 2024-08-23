using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Dtos.CreateQuiz;
using QuizBackend.Application.Interfaces;

namespace QuizBackend.Api.Controllers
{
    public class QuizController : BaseController
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpPost("generate-quiz")]
        public async Task<IActionResult> GenerateQuizFromPromptTemplateAsync(QuizArgumentsDto quizArguments)
        {
            if (quizArguments == null)
            {
                return BadRequest();
            }

            var jsonResponse = await _quizService.GenerateQuizFromPromptTemplateAsync(quizArguments);
            return Ok(jsonResponse);
        }
    }
}
