using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Quizzes.JoinQuiz;
public record JoinQuizCommand(string JoinCode, string? UserName) : ICommand<JoinQuizResponse>;