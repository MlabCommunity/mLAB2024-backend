using MediatR;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Quizzes.DeleteQuiz;

public record DeleteQuizCommand(Guid Id) : ICommand<Unit>;