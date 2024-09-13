using QuizBackend.Application.Interfaces.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;


namespace QuizBackend.Application.Queries.QuizzesParticipations.GetUserAnswer;

public record GetUserQuizHistoryQuery(string ParticipantId) : IQuery<List<QuizParticipationHistoryResponse>>;
