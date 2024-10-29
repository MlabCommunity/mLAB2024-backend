namespace QuizBackend.Domain.Exceptions;

public class ForbidException : Exception
{
    public string ResourceName { get; }
    public string ActionAttempted { get; }

    public ForbidException(string message, string resourceName, string actionAttempted)
        : base(message)
    {
        ResourceName = resourceName;
        ActionAttempted = actionAttempted;
    }

    public ForbidException(string message, string resourceName, string actionAttempted, Exception innerException)
        : base(message, innerException)
    {
        ResourceName = resourceName;
        ActionAttempted = actionAttempted;
    }

}