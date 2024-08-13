using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Application.Dtos.Profile
{
    public class UserProfileDto
    {
        public required string Id { get; set; }
        public required string Email { get; set; } 
        public required string UserName { get; set; }
    }
}
