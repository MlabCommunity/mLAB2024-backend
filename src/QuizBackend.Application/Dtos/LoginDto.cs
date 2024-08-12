using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Application.Dtos
{
    public class LoginDto
    {
        public required string? Email { get; set; }

        [DataType(DataType.Password)]
        public required string? Password { get; set; }
    }
}
