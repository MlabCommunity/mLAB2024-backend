using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Domain.Exceptions
{
    public class BadRequestException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }
        public BadRequestException(string message) : base(message)
        {
            Errors = new Dictionary<string, string[]>();
        }
        public BadRequestException(string message, IEnumerable<string> errors) : base(message)
        {
            Errors = new Dictionary<string, string[]>
            {
                { "errors", errors.ToArray() }
            };
        }
        public BadRequestException(string message, IDictionary<string, string[]> errors) : base(message)
        {
            Errors = errors ?? new Dictionary<string, string[]>();
        }
    }
}
