using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Application.Dtos.Profile
{
    public record UpdateUserProfileRequest(string UserName)
    {
    }
}
