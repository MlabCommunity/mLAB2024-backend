using MediatR;

namespace QuizBackend.Application.Interfaces.Messaging
{
    public interface ICommand : IRequest<Unit>, IBaseCommand;
    public interface ICommand<TResponse> : IRequest<TResponse>;
    public interface IBaseCommand;
}
