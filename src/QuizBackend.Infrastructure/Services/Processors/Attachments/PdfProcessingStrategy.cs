using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Http;
using QuizBackend.Infrastructure.Interfaces;
using System.Text;

namespace QuizBackend.Infrastructure.Services.Processors.Attachments;
public class PdfProcessingStrategy : IAttachmentProcessingStrategy
{
    public async Task<string> ProcessFile(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        var text = new StringBuilder();

        using var pdfReader = new PdfReader(memoryStream);
        using var pdfDocument = new PdfDocument(pdfReader);

        var pageCount = pdfDocument.GetNumberOfPages();

        for (int i = 1; i <= pageCount; i++)
        {
            var page = pdfDocument.GetPage(i);
            var strategy = new SimpleTextExtractionStrategy();
            var pageText = PdfTextExtractor.GetTextFromPage(page, strategy);
            text.AppendLine(pageText);
        }

        return text.ToString();
    }
}