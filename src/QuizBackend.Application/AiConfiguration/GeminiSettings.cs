using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Application.AiConfiguration
{
    public class GeminiSettings
    {
        public required string Endpoint { get; set; }
        public required string ApiKey { get; set; }
        public required string ModelId { get; set; }
    }
}
