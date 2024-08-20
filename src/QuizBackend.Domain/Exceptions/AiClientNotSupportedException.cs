using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Domain.Exceptions
{
    public class AiClientNotSupportedException : NotSupportedException
    {
        public AiClientNotSupportedException() { }
        public AiClientNotSupportedException(string message) : base(message) { }
    }
}
