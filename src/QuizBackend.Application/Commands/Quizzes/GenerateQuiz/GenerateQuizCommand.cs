using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Enums;

namespace QuizBackend.Application.Commands.Quizzes.GenerateQuiz;

public record GenerateQuizCommand(
    string? Content,
    int NumberOfQuestions,
    QuizLanguage Language,
    List<QuestionType> QuestionTypes,
    List<IFormFile>? Attachments
) : ICommand<GenerateQuizResponse>;