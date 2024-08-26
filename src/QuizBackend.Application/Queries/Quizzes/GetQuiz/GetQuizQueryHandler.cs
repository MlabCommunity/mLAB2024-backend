using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Queries.Quizzes.GetQuiz
{
    public class GetQuizQueryHandler : IQueryHandler<GetQuizQuery, QuizDetailsDto>
    {
        private readonly IQuizRepository _quizRepository;

        public GetQuizQueryHandler(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<QuizDetailsDto> Handle(GetQuizQuery request, CancellationToken cancellationToken)
        {
            var quiz = await _quizRepository.Get(request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Quiz), request.Id.ToString());

            var questionsDto = quiz.Questions
                .Select(q => new QuestionDto(
                     q.Id,
                     q.Title,
                     q.Description,
                     q.Answers.Select(a => new AnswerDto(
                         a.Id,
                         a.Content,
                         a.IsCorrect
                     )).ToList()
                 ))
                 .ToList();

            var quizDto = new QuizDetailsDto(
                quiz.Id,
                quiz.Title,
                quiz.Description,
                quiz.Availability,
                quiz.Status,
                questionsDto);

            return quizDto;

        }
    }
}
