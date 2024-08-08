using QuizBackend.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Application.Interfaces
{
    public interface IProfileService
    {
        Task<UserProfileDto> GetProfileAsync();
    }
}
