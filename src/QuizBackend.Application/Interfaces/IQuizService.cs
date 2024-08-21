using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Application.Interfaces
{
    public interface IQuizService
    {
        Task<string> GetTextFromPromptAsync(string prompt);
    }
}
