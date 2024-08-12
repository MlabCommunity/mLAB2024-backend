using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Domain.Exceptions
{
    public class BadRequestException : Exception
    {
        public IEnumerable<string> Errors { get; }

        public BadRequestException(string message) : base(message)
        {
            Errors = [];
        }

        public BadRequestException(string message, IEnumerable<string> errors) : base(message)
        {
            Errors = errors ?? [];
        }
    }
}
