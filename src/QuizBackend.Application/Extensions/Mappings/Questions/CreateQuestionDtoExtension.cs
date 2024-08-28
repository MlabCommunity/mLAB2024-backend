using QuizBackend.Application.Dtos.Quizzes.CreateQuiz;
using QuizBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Application.Extensions.Mappings.Questions
{
    public static class CreateQuestionDtoExtension
    {
        public static Question ToEntity(this CreateQuestionDto dto)
        {
            return new Question
            {
                Id = 
            }
        }
    }
}
