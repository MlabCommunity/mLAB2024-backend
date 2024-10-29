using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Enums;

namespace QuizBackend.Application.Commands.Quizzes.CreateQuiz;

public record CreateQuizCommand(
    string Title,
    string Description,
    QuestionType QuestionType,
    List<CreateQuizQuestion> CreateQuizQuestions
    ) : ICommand<CreateQuizResponse>;

public record CreateQuizQuestion(
    string Title,
    List<CreateQuizAnswer> CreateQuizAnswers);
public record CreateQuizAnswer(
    string Content,
    bool IsCorrect);