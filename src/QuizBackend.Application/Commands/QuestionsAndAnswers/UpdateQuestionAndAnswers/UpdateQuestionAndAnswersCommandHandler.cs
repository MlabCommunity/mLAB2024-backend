using QuizBackend.Application.Extensions.Mappings.QuestionAndAnswers;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Commands.QuestionsAndAnswers.UpdateQuestionAndAnswers
{
    public class UpdateQuestionAndAnswersCommandHandler : ICommandHandler<UpdateQuestionAndAnswersCommand, Guid>
    {
        private readonly IQuestionAndAnswersRepository _questionAndAnswersRepository;

        public UpdateQuestionAndAnswersCommandHandler(IQuestionAndAnswersRepository questionAndAnswersRepository)
        {
            _questionAndAnswersRepository = questionAndAnswersRepository;
        }

        public async Task<Guid> Handle(UpdateQuestionAndAnswersCommand request, CancellationToken cancellationToken)
        {
            var questionEntity = await _questionAndAnswersRepository.GetById(request.Id)
                ?? throw new NotFoundException(nameof(Question), request.Id.ToString());

            request.UpdateEntity(questionEntity);
            await _questionAndAnswersRepository.Update(questionEntity);

            return questionEntity.Id;
        }
    }
}
