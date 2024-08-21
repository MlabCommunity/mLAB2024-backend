using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("GenerateFromPrompt")]
        public async Task<string> GenerateResponseFromPromptAsync([FromBody] string prompt)
        {

            var response = await _quizService.GetTextFromPromptAsync(prompt);

            return response;
        }
    }
}
