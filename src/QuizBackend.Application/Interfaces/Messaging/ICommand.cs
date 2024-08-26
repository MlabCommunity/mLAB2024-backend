using MediatR;

namespace QuizBackend.Application.Interfaces.Messaging
{
    public interface ICommand : IRequest<Unit>, IBaseCommand;
    public interface ICommand<out TResponse> : IRequest<TResponse>, IBaseCommand;
    public interface IBaseCommand;
}
