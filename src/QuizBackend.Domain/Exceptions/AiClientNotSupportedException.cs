
namespace QuizBackend.Domain.Exceptions
{
    public class AiClientNotSupportedException : NotSupportedException
    {
        public AiClientNotSupportedException() { }
        public AiClientNotSupportedException(string message) : base(message) { }
    }
}
