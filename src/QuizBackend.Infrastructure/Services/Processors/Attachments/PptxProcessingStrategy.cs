using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Http;
using QuizBackend.Infrastructure.Interfaces;
using System.Text;

namespace QuizBackend.Infrastructure.Services.Processors.Attachments;

public class PptxProcessingStrategy : IAttachmentProcessingStrategy
{
    public async Task<string> ProcessFile(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        var text = new StringBuilder();

        using (PresentationDocument presentationDocument = PresentationDocument.Open(memoryStream, false))
        {
            foreach (SlidePart slidePart in presentationDocument.PresentationPart!.SlideParts)
            {
                foreach (var textElement in slidePart.Slide.Descendants<DocumentFormat.OpenXml.Drawing.Text>())
                {
                    text.AppendLine(textElement.Text);
                }
            }
        }

        return text.ToString();
    }
}