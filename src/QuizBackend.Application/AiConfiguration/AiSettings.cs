using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Application.AiConfiguration
{
    public class AiSettings
    {
        public required string Provider { get; set; }
        public OpenAISettings OpenAI { get; set; }
        public AzureOpenAISettings AzureOpenAI { get; set; }
        public GeminiSettings Gemini { get; set; }
    }
}
