using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Interfaces;
using QuizBackend.Infrastructure.Interfaces;

namespace QuizBackend.Infrastructure.Services.Processors.Attachments;

public class AttachmentProcessor : IAttachmentProcessor
{
    private readonly Dictionary<string, IAttachmentProcessingStrategy> _strategies;
    public AttachmentProcessor()
    {
        _strategies = new Dictionary<string, IAttachmentProcessingStrategy>
        {
            { ".pdf", new PdfProcessingStrategy() },
            { ".txt", new TxtProcessingStrategy() },
            { ".docx", new DocxProcessingStrategy() },
            { ".pptx", new PptxProcessingStrategy() },
            { ".xlsx", new ExcelProcessingStrategy() }
        };
    }
    public async Task<List<string>> ProcessAttachments(List<IFormFile> files)
    {
        var processedAttachments = await Task.WhenAll(files.Select(ProcessSingleAttachment));
        return processedAttachments.ToList();
    }
    private async Task<string> ProcessSingleAttachment(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (_strategies.TryGetValue(extension, out var strategy))
        {
            return await strategy.ProcessFile(file);
        }

        return await new TxtProcessingStrategy().ProcessFile(file);
    }
}