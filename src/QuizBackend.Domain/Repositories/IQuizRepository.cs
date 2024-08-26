using QuizBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Domain.Repositories
{
    public interface IQuizRepository
    {
        Task AddAsync(Quiz quiz);
    }
}
