using MediatR;

namespace QuizBackend.Application.Interfaces.Messaging
{
    public interface IQuery<TResponse> : IRequest<TResponse>;
}
