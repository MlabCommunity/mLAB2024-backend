﻿using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Queries.Quizzes.GetQuiz
{
    public record GetQuizQuery(Guid Id) : IQuery<QuizDetailsDto>;
}