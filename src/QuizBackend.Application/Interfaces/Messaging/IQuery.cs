using MediatR;

namespace QuizBackend.Application.Interfaces.Messaging
{
    public interface IQuery<out TResponse> : IRequest<TResponse>;
}
