﻿using QuizBackend.Application.Dtos.Quizzes.CreateQuiz;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Quizzes.GenerateQuiz
{
    public class GenerateQuizCommandHandler : ICommandHandler<GenerateQuizCommand, CreateQuizDto>
    {
        private readonly IQuizService _quizService;
        private readonly IAttachmentProcessor _attachmentProcessor;
        private const int MaxContentLength = 10000;

        public GenerateQuizCommandHandler(IQuizService quizService, IAttachmentProcessor attachmentProcessor)
        {
            _quizService = quizService;
            _attachmentProcessor = attachmentProcessor;
        }

        public async Task<CreateQuizDto> Handle(GenerateQuizCommand command, CancellationToken cancellationToken)
        {
            string content = command.Content;

            if (command.Attachments != null && command.Attachments.Count > 0)
            {
                var processedAttachments = await _attachmentProcessor.ProccessAttachments(command.Attachments);
                var attachmentContents = processedAttachments.Select(pa => pa.Content).ToList();

                content = $"{content}\n\n{string.Join("\n\n", attachmentContents)}";
            }

            if (content.Length > MaxContentLength)
            {
                content = content.Substring(0, MaxContentLength);
            }

            var updatedCommand = command with { Content = content };

            var quizDto = await _quizService.GenerateQuizFromPromptTemplateAsync(updatedCommand);
            return quizDto;
        }
    }
}
