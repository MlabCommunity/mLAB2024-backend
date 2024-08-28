using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Extensions.Mappings.Quizzes;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Application.Interfaces.Users;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;
using System.Security.Claims;

namespace QuizBackend.Application.Commands.Quizzes.CreateQuiz
{
    public class CreateQuizCommandHandler : ICommandHandler<CreateQuizCommand, Guid>
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IUserContext _userContext;

        public CreateQuizCommandHandler(IQuizRepository quizRepository, IUserContext userContext)
        {
            _quizRepository = quizRepository;
            _userContext = userContext;
        }

        public async Task<Guid> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            var ownerId = _userContext.UserId;

            var quiz = request.QuizDto.ToEntity(ownerId);
            
            await _quizRepository.AddAsync(quiz);

            return quiz.Id;
        }
    }
}
