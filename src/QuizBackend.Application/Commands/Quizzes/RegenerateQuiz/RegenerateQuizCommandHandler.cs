using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Interfaces.Messaging;
using System.Text.RegularExpressions;

namespace QuizBackend.Application.Commands.Quizzes.RegenerateQuiz;

public class RegenerateQuizCommandHandler : ICommandHandler<RegenerateQuizCommand, GenerateQuizResponse>
{
    private readonly IQuizService _quizService;

    public RegenerateQuizCommandHandler(IQuizService quizService)
    {
        _quizService = quizService;
    }

    public async Task<GenerateQuizResponse> Handle(RegenerateQuizCommand command, CancellationToken cancellationToken)
    {
        var quizDto = await _quizService.RegenerateQuizFromPromptTemplate();
        var updatedQuestions = new List<GenerateQuestion>();

        foreach (var question in quizDto.GenerateQuestions)
        {
            var updatedQuestion = question with { Title = RemoveNumberPrefixFromReGeneration(question.Title) };
            updatedQuestions.Add(updatedQuestion);
        }
        return quizDto with { GenerateQuestions = updatedQuestions };
    }

    private string RemoveNumberPrefixFromReGeneration(string title)
    {
        return Regex.Replace(title, @"^\d+\.\s+", string.Empty);
    }
}