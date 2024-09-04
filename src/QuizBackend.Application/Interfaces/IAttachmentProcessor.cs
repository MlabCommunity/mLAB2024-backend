using Microsoft.AspNetCore.Http;

namespace QuizBackend.Application.Interfaces;
public interface IAttachmentProcessor
{
    Task<List<string>> ProcessAttachments(List<IFormFile> files);
}
