using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Interfaces;
using System.Text;
using iText.Kernel.Pdf;

namespace QuizBackend.Infrastructure.Services.Processors;
public class AttachmentProcessor : IAttachmentProcessor
{
    public async Task<List<string>> ProcessAttachments(List<IFormFile> files)
    {
        var processedAttachments = await Task.WhenAll(files.Select(ProcessSingleAttachment));
        return [.. processedAttachments];
    }

    private async Task<string> ProcessSingleAttachment(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return extension switch
        {
            ".pdf" => await ProcessPdfFile(file),
            ".txt" => await ProcessTxtFile(file),
            _ => await ProcessTxtFile(file),
        };
    }

    private async Task<string> ProcessTxtFile(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        using var reader = new StreamReader(memoryStream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, leaveOpen: true);
        return await reader.ReadToEndAsync();
    }

    private async Task<string> ProcessPdfFile(IFormFile file)
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
