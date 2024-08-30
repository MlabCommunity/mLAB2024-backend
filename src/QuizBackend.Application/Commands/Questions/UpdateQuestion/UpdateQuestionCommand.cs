﻿using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Questions.UpdateQuestion
{
    public record UpdateQuestionCommand(Guid Id, string Title, List<UpdateAnswer> UpdateAnswers) : ICommand<Guid>;
    public record UpdateAnswer(Guid Id, string Content, bool IsCorrect, Guid QuestionId);
}
