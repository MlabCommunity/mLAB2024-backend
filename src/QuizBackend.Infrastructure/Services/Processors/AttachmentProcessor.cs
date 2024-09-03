using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Entities;
using System.Text;

namespace QuizBackend.Infrastructure.Services.Processors
{
    public class AttachmentProcessor : IAttachmentProcessor
    {
        public AttachmentProcessor()
        {
           
        }
        public async Task<List<ProcessedAttachment>> ProccessAttachments(List<IFormFile> files)
        {
            var processedAttachments = new List<ProcessedAttachment>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var tempFilePath = Path.GetTempFileName();

                    using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    var processedAttachment = await ProccessSingleAttachment(tempFilePath);

                    processedAttachments.Add(processedAttachment);

                    File.Delete(tempFilePath);
                }
            }
            return processedAttachments;
        }
        private async Task<ProcessedAttachment> ProccessSingleAttachment(string filePath)
        {
            var contentBuilder = new StringBuilder();
            const int BufferSize = 8 * 1024;

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(fileStream, Encoding.UTF8))
            {
                char[] buffer = new char[BufferSize];
                int read;
                while ((read = await reader.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    contentBuilder.Append(buffer, 0, read);
                }
            }

            string fileName = Path.GetFileName(filePath);
            string content = contentBuilder.ToString();

            return new ProcessedAttachment
            {
                FileName = fileName,
                ContentType = GetContentType(fileName),
                Content = content
            };
        }

        private string GetContentType(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();
            return extension switch
            {
                ".pdf" => "application/pdf",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                _ => "application/octet-stream",
            };
        }
    }
}
