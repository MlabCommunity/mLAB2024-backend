
namespace QuizBackend.Domain.Exceptions
{
    public class ArgumentIsNullException : ArgumentNullException
    {
        public string ParamName { get; }

        public ArgumentIsNullException(string paramName)
            : base(ValidateParam(paramName))
        {
            ParamName = paramName;
        }

        public ArgumentIsNullException(string paramName, string message)
            : base(message)
        {
            ParamName = paramName;
        }

        public ArgumentIsNullException(string paramName, string message, Exception innerException)
            : base(message, innerException)
        {
            ParamName = paramName;
        }

        private static string ValidateParam(string paramName)
        {
            if (string.IsNullOrWhiteSpace(paramName))
            {
                throw new ArgumentException("Parameter name cannot be null, empty, or whitespace.", nameof(paramName));
            }
            return $"Parameter '{paramName}' cannot be null or whitespace.";
        }
    }
}