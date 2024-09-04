using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Interfaces;
using System.Text;

namespace QuizBackend.Infrastructure.Services.Processors;
public class AttachmentProcessor : IAttachmentProcessor
{
    public async Task<List<string>> ProcessAttachments(List<IFormFile> files)
    {
        var processedAttachments = new List<string>();

        foreach (var file in files)
        {
            var processedAttachment = await ProcessSingleAttachment(file);
            processedAttachments.Add(processedAttachment);
        }

        return processedAttachments;
    }
    private async Task<string> ProcessSingleAttachment(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        string content;
        using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
        {
            content = await reader.ReadToEndAsync();
        }
        await memoryStream.FlushAsync();

        return content;
    }
}