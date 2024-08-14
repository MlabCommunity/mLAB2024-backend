using System.ComponentModel.DataAnnotations;

namespace QuizBackend.Application.Dtos
{
    public class LoginDto
    {
       public required string Email { get; set; }

        [DataType(DataType.Password)]
       public required string Password { get; set; }
    }
}
