﻿using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Questions.UpdateQuestion
{
    public record UpdateQuestionCommand(Guid Id, string Title) : ICommand<Guid>;
}
