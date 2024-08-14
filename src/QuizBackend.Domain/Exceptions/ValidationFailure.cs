namespace QuizBackend.Domain.Exceptions
{
    public class ValidationFailure
    {
        public string PropertyName { get; set; } = default!;
        public string ErrorMessage { get; set; } = default!;
    }
}
