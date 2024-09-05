using Microsoft.AspNetCore.Http;

namespace QuizBackend.Infrastructure.Interfaces;
public interface IAttachmentProcessingStrategy
{
    Task<string> ProcessFile(IFormFile file);
}
