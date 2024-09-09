using Microsoft.AspNetCore.Http;
using QuizBackend.Infrastructure.Interfaces;
using System.Text;

namespace QuizBackend.Infrastructure.Services.Processors.Attachments;

public class TxtProcessingStrategy : IAttachmentProcessingStrategy
{
    public async Task<string> ProcessFile(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        using var reader = new StreamReader(memoryStream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, leaveOpen: true);
        return await reader.ReadToEndAsync();
    }
}