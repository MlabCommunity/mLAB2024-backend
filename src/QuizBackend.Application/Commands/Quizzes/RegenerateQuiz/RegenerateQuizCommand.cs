using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Quizzes.RegenerateQuiz;

public record RegenerateQuizCommand() : ICommand<GenerateQuizResponse>;