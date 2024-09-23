using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Interfaces.Messaging;

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
        return quizDto;
    }
}