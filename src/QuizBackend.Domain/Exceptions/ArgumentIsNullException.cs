using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Domain.Exceptions
{
    public class ArgumentIsNullException : ArgumentNullException
    {
        public string ParamName { get; }

        public ArgumentIsNullException(string paramName)
            : base($"Parameter '{paramName}' cannot be null.")
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
    }
}
