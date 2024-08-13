using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Application.Dtos
{
    public record UpdateUserProfileRequest(string UserName)
    {
    }
}
