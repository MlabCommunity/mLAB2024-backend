namespace QuizBackend.Domain.Entities
{
    public class ProcessedAttachment
    {
        public string FileName { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public string Content { get; set; } = default!;
    }
}
