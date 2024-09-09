using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using QuizBackend.Infrastructure.Interfaces;
using System.Text;

namespace QuizBackend.Infrastructure.Services.Processors.Attachments;

public class DocxProcessingStrategy : IAttachmentProcessingStrategy
{
    public async Task<string> ProcessFile(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        using var wordDoc = WordprocessingDocument.Open(memoryStream, false);
        var body = wordDoc.MainDocumentPart?.Document.Body;
        var text = new StringBuilder();

        foreach (var paragraph in body!.Elements<Paragraph>())
        {
            text.AppendLine(paragraph.InnerText);
        }

        return text.ToString();
    }
}