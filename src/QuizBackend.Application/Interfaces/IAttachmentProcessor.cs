using Microsoft.AspNetCore.Http;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Application.Interfaces
{
    public interface IAttachmentProcessor
    {
        Task<List<ProcessedAttachment>> ProccessAttachments(List<IFormFile> attachments);
    }
}
