using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.QuizzesParticipations.JoinQuiz;

public record JoinQuizCommand(string JoinCode) : ICommand<JoinQuizResponse>;