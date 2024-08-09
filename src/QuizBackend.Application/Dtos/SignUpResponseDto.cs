using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Application.Dtos
{
    public class SignUpResponseDto
    {
        public bool Succeed { get; set; }
        public string? UserId { get; set; }
        public string[] Errors { get; set; } = [];
    }
}
