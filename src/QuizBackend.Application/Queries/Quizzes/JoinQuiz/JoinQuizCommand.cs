using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Queries.Quizzes.JoinQuiz;

public record JoinQuizCommand(string JoinCode) : ICommand<JoinQuizResponse>;