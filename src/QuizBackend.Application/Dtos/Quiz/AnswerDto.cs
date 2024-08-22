using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Application.Dtos.Quiz
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool Iscorrect { get; set; }
        public int QuestionId { get; set; }
        public QuestionDto Question { get; set; }
    }
}
