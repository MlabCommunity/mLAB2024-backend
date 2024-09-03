using QuizBackend.Application.Extensions.Mappings.QuestionAndAnswers;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Commands.QuestionsAndAnswers.CreateQuestionAndAnswers
{
    public class CreateQuestionAndAnswersCommandHandler : ICommandHandler<CreateQuestionAndAnswersCommand, Guid>
    {
        private readonly IQuestionAndAnswersRepository _questionAndAnswersRepository;

        public CreateQuestionAndAnswersCommandHandler(IQuestionAndAnswersRepository questionAndAnswersRepository)
        {
            _questionAndAnswersRepository = questionAndAnswersRepository;
        }

        public async Task<Guid> Handle(CreateQuestionAndAnswersCommand request, CancellationToken cancellationToken)
        {
            var question = request.ToEntity();

            await _questionAndAnswersRepository.Add(question);

            return question.Id;
        }
    }
}