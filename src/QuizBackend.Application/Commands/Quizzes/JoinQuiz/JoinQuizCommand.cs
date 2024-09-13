using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Quizzes.JoinQuiz;
public record JoinQuizCommand(string? UserName, string JoinCode) : ICommand<JoinQuizResponse>;
