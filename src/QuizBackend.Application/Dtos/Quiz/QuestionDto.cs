using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Application.Dtos.Quiz
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<AnswerDto> Answers { get; set; }
        public Guid QuizId { get; set; }
        public QuizDto Quiz { get; set; }
    }
}
